using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : PersistentSingleton<WaveManager> , IInitialize{
    
    public event System.EventHandler<WaveEventArgs> WaveIsStart
    {
        add
        {
            _waveIsEnded += value;
        }
        remove
        {
            _waveIsStarted -= value;
        }
    }
    public event System.EventHandler<WaveEventArgs> WaveIsEnd
    {
        add
        {
            _waveIsEnded += value;
        }
        remove
        {
            _waveIsEnded -= value;
        }
    }
    public event System.EventHandler<WaveEventArgs> WaveIdleIsStart
    {
        add
        {
            _waveIdleIsStarted += value;
        }
        remove
        {
            _waveIdleIsStarted -= value;
        }
    }
    public event System.EventHandler<WaveEventArgs> WaveIdleIsEnd
    {
        add
        {
            _waveIdleIsEnded += value;
        }
        remove
        {
            _waveIdleIsEnded -= value;
        }
    }

    public int CurrentWave { get { return _currentWave; } }
    public bool WaveIdleIsProgress { get { return _waveIdleIsProgress; } }
    public bool WaveIsProgress { get { return _waveIsProgress; } }
    public float WaveTimer { get { return _waveTime; } }

    private event System.EventHandler<WaveEventArgs> _waveIdleIsStarted;
    private event System.EventHandler<WaveEventArgs> _waveIdleIsEnded;
    private event System.EventHandler<WaveEventArgs> _waveIsStarted;
    private event System.EventHandler<WaveEventArgs> _waveIsEnded;


    public List<GameObject> ActiveEnemies { get; private set;}
    public List<GameObject> ActivePickupItems { get; private set; }

    [SerializeField]private int _currentWave;
    [SerializeField]private bool _waveIsProgress;
    [SerializeField]private bool _waveIdleIsProgress;
    [SerializeField]private float _waveTime;
    [SerializeField]private int _remainsEnemies;
    [SerializeField]private List<Transform> _enemySpawnPoints = new List<Transform>();


    public void Initialize()
    {
        ActiveEnemies = new List<GameObject>();
        ActivePickupItems = new List<GameObject>();
        _waveIsProgress = false;
        _currentWave = 0;
        _waveTime = 0;
        _remainsEnemies = 0;
        GUIManager.Instance.SetWave(_currentWave);
        GUIManager.Instance.RefreshTimeWave(_waveTime);
        GUIManager.Instance.RefreshRemainsEnemies(_remainsEnemies);
    }
    public void Disable()
    {
        
    }

    public void LoadIdleWave(int wave)
    {
        _currentWave = wave;
        GUIManager.Instance.SetWave(_currentWave);
        GUIManager.Instance.SetActiveRemainsEnemiesText(false);
        GUIManager.Instance.SetActiveTimeText(true);

        StartCoroutine(LoadWaveIdle(_currentWave));
    }
    public void LoadBattleWave(int wave)
    {
        _currentWave = wave;
        GUIManager.Instance.SetWave(_currentWave);
        GUIManager.Instance.SetActiveRemainsEnemiesText(true);
        GUIManager.Instance.SetActiveTimeText(false);

        StartCoroutine(LoadWaveBattle(_currentWave));
    }

    IEnumerator LoadWaveIdle(int index)
    {
        _waveIdleIsProgress = true;
        if (_waveIdleIsStarted != null)
            _waveIdleIsStarted(this, null);

        int pickupItemsCount = GameConstants.WaveInfo.Wave[index].pickupCount;
        float timer= GameConstants.WaveInfo.IdleTime;

        for (int i = 0; i < pickupItemsCount; i++)
        {
            float x = Random.Range(GameConstants.PickupItem.pickupSpawnMinPos.x, GameConstants.PickupItem.pickupSpawnMaxPos.x);
            float y = Random.Range(GameConstants.PickupItem.pickupSpawnMinPos.y, GameConstants.PickupItem.pickupSpawnMaxPos.y);
            Vector2 randomPosition = new Vector2(x, y);
            int randomItem = Random.Range(0, GameConstants.PickupItem.ConsumablesNames.Length);
            PoolManager.GetObject(GameConstants.PickupItem.ConsumablesNames[randomItem],new Vector3(randomPosition.x,randomPosition.y,0), Quaternion.identity);
        }

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            GUIManager.Instance.RefreshTimeWave(timer);
            yield return new WaitForEndOfFrame();
        }

        _waveIdleIsProgress = false;
        if (_waveIdleIsEnded != null)
            _waveIdleIsEnded(this, new WaveEventArgs(_currentWave));
    }

    IEnumerator LoadWaveBattle(int index)
    {
        _waveIsProgress = true;
        if (_waveIsStarted != null)
            _waveIsStarted(this, null);

        _remainsEnemies = GameConstants.WaveInfo.Wave[index].enemyCount;

        int spawnEnemiesCount = GameConstants.WaveInfo.Wave[index].enemyCount;
        float timer = GameConstants.WaveInfo.Wave[index].timeWave;
        float timeRespawn = timer / _remainsEnemies;
        float timerRes = timeRespawn;
        string[] enemiesNames = GameConstants.Enemy.EnemiesNames;

        GUIManager.Instance.RefreshRemainsEnemies(_remainsEnemies);



        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerRes -= Time.deltaTime;
            if(timerRes < 0)
            {
                timerRes = timeRespawn;
                int randomEnemyIndex = Random.Range(0, enemiesNames.Length);
                int randomSpawnIndex = Random.Range(0, _enemySpawnPoints.Count);

                GameObject obj = PoolManager.GetObject(enemiesNames[randomEnemyIndex], _enemySpawnPoints[randomSpawnIndex].position, Quaternion.identity);
                EnemyController controller = obj.GetComponent<EnemyController>();

                ActiveEnemies.Add(obj);
                controller.OnDestroyed += EnemyDestroyed;

                spawnEnemiesCount--;

                if (spawnEnemiesCount == 0)
                    break;
            }

            GUIManager.Instance.RefreshTimeWave(timer);
            yield return new WaitForEndOfFrame();
        }

        while(ActiveEnemies.Count > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        _waveIsProgress = false;
        if(_waveIsEnded != null)
            _waveIsEnded(this,new WaveEventArgs(_currentWave));
    }


    private void EnemyDestroyed(object obj , UnitArgs e)
    {
        if(ActiveEnemies.Contains(e.unit))
        {
            EnemyController controller = e.unit.GetComponent<EnemyController>();
            controller.OnDestroyed -= EnemyDestroyed;

            _remainsEnemies--;
            GUIManager.Instance.RefreshRemainsEnemies(_remainsEnemies);
            ActiveEnemies.Remove(e.unit);
        }

    }

}

public class GameOjectInfo
{
    public GameObject gameObj;
    public Transform position;
    public Quaternion rotation;

    public GameOjectInfo(GameObject obj, Transform pos, Quaternion rot)
    {
        gameObj = obj;
        position = pos;
        rotation = rot;
    }
}

