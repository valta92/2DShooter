using UnityEngine;
using System;
using System.Collections;

public class GameManager : PersistentSingleton<GameManager> , IInitialize
{

    public bool Paused { get { return _paused; } }

    [SerializeField]private bool _paused;
    [SerializeField]private bool _gameStarted;

    private PlayerController _playerController;
    private CameraController _camera;


    public void Initialize()
    {
        WaveManager.Instance.WaveIdleIsEnd += OnIdleWaveIsEnded;
        WaveManager.Instance.WaveIsEnd += OnBattleWaveIsEnded;
    }
    public void Disable()
    {
        WaveManager.Instance.WaveIdleIsEnd -= OnIdleWaveIsEnded;
        WaveManager.Instance.WaveIsEnd -= OnBattleWaveIsEnded;
    }

    private void OnIdleWaveIsEnded(object obj,WaveEventArgs e)
    {
        WaveManager.Instance.LoadBattleWave(e.lastWave);
    }

    private void OnBattleWaveIsEnded(object obj,WaveEventArgs e)
    {
        WaveManager.Instance.LoadIdleWave(e.lastWave + 1);
    }

    public void StartGame()
    {
        ScoreManager.Instance.SetScore(0);
        SetPause(false);
        _gameStarted = true;
        GUIManager.Instance.SetActiveHUDWindow(true);
        GUIManager.Instance.SetActiveInputWindow(true);
        GUIManager.Instance.SetActiveGameOverWindow(false);

        GameObject player = Instantiate(Resources.Load(GameConstants.Player.PlayerPrefabPath)) as GameObject;
        player.transform.position = GameConstants.Player.SpawnPlayerPosition;
        player.transform.rotation = Quaternion.identity;

        if(_camera == null)
            _camera = Camera.main.gameObject.GetComponent<CameraController>() as CameraController;

        if(_playerController == null)
            _playerController = player.GetComponent<PlayerController>();

        _playerController.Initialize();
        _camera.Initialize();
        WaveManager.Instance.LoadIdleWave(1);

    }
    public void GameOver()
    {
        PoolManager.DeactivateAll();
        StopAllCoroutines();
        LoadManager.Instance.GoToLevel(0);
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

    void OnApplicationFocus( bool hasFocus )
    {
        if(_gameStarted)
        {
            SetPause(!hasFocus);
        }

    }

}     