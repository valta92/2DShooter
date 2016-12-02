using UnityEngine;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager> 
{

    public bool Paused { get { return _paused; } }
    public int Score { get { return _score; } }

    [SerializeField]private bool _paused;
    [SerializeField]private int _score;

    private PlayerController _playerController;
    private CameraController _camera;


    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Reset();
        GUIManager.Instance.SetActiveHUDWindow(true);
        GUIManager.Instance.SetActiveInputWindow(true);
        GUIManager.Instance.SetActiveGameOverWindow(false);

        GameObject player = Instantiate(Resources.Load(GameConstants.Player.PlayerPrefabPath)) as GameObject;
        player.transform.position = GameConstants.Player.SpawnPlayerPosition;
        player.transform.rotation = Quaternion.identity;

        _playerController = player.GetComponent<PlayerController>();
        _playerController.Initialize();

        _camera = Camera.main.gameObject.GetComponent<CameraController>() as CameraController;
        _camera.Initialize();

        LevelManager.Instance.Initialize();

    }
    public void GameOver()
    {
        PoolManager.DeactivateAll();
        StopAllCoroutines();
        LoadManager.Instance.GoToLevel(0);
    }

    public void AddScore(int count)
    {
        _score += count;
        GUIManager.Instance.RefreshScore(_score);
    }
    public void SetScore(int count)
    {
        _score = count;
        GUIManager.Instance.RefreshScore(_score);
    }

    public void SetPause(bool value)
    {
        if(value)
        {
            _paused = true;
            Time.timeScale = 0;
        }
        else
        {
            _paused = false;
            Time.timeScale = 1;
        }
            
    }

    public void Reset()
    {
        SetScore(0);
        SetPause(false);
    }
}     