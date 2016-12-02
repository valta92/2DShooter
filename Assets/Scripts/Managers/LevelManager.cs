using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : PersistentSingleton<LevelManager> {
    
    public event System.EventHandler WaveIsStarted
    {
        add
        {
            WaveIsStarted += value;
        }
        remove
        {
            _waveIsStarted -= value;
        }
    }
    public event System.EventHandler WaveIsEnded
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
    public event System.EventHandler WaveIdleIsStarted
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
    public event System.EventHandler WaveIdleIsEnded
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

    private event System.EventHandler _waveIdleIsStarted;
    private event System.EventHandler _waveIdleIsEnded;
    private event System.EventHandler _waveIsStarted;
    private event System.EventHandler _waveIsEnded;


    [SerializeField]private int _currentWave;
    [SerializeField]private bool _waveIsProgress;
    [SerializeField]private bool _waveIdleIsProgress;
    [SerializeField]private float _waveTime;
    [SerializeField]private List<Transform> _enemySpawnPoints = new List<Transform>();

    public void Initialize()
    {
        _waveIsProgress = false;
        _currentWave = 1;
        SetWave(_currentWave);
    }

    public void NextWave()
    {
        _currentWave++;
        GUIManager.Instance.SetWave(_currentWave);
        StartCoroutine(LoadWaveIdle(_currentWave));
    }

    public void SetWave(int wave)
    {
        _currentWave = wave;
        GUIManager.Instance.SetWave(_currentWave);
        StartCoroutine(LoadWaveIdle(_currentWave));
    }

    IEnumerator LoadWaveIdle(int index)
    {
        _waveIdleIsProgress = true;
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
        StartCoroutine(LoadWave(index));
    }

    IEnumerator LoadWave(int index)
    {
        _waveIsProgress = true;

        int remainsEnemies = GameConstants.WaveInfo.Wave[index].enemyCount;
        float timer = GameConstants.WaveInfo.Wave[index].timeWave;
        float timeRespawn = timer / remainsEnemies;
        float timerRes = timeRespawn;
        string[] enemiesNames = GameConstants.Enemy.EnemiesNames;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerRes -= Time.deltaTime;
            if(timerRes < 0)
            {
                timerRes = timeRespawn;
                int randomEnemyIndex = Random.Range(0, enemiesNames.Length);
                int randomSpawnIndex = Random.Range(0, _enemySpawnPoints.Count);

                PoolManager.GetObject(enemiesNames[randomEnemyIndex], _enemySpawnPoints[randomSpawnIndex].position, Quaternion.identity);
                remainsEnemies--;

                if (remainsEnemies == 0)
                    break;
            }

            GUIManager.Instance.RefreshTimeWave(timer);
            yield return new WaitForEndOfFrame();
        }

        _waveIsProgress = false;



    }
}
