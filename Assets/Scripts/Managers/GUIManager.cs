using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : PersistentSingleton<GUIManager> , IInitialize
{


    [SerializeField]private GameObject _HUDWindow;
    [SerializeField]private GameObject _InputWindow;
    [SerializeField]private GameObject _GameOverWindow;
    [SerializeField]private Text _scoreText;
    [SerializeField]private Text _ammoText;
    [SerializeField]private Text _healthText;
    [SerializeField]private Image _weaponImage;
    [SerializeField]private Text _timeWaveText;
    [SerializeField]private Text _waveText;
    [SerializeField]private Text _remainsEnemiesText;

    public void Initialize()
    {
        
    }
    public void Disable()
    {
        
    }

    public void RefreshRemainsEnemies(int value)
    {
        _remainsEnemiesText.text = GameConstants.UI.HUDText.RemainsEnemies + value;
    }

    public void SetActiveInputWindow(bool value)
    {
        _InputWindow.SetActive(value);
    }

    public void SetActiveGameOverWindow(bool value)
    {
//        _GameOverWindow.SetActive(value);
    }

    public void SetActiveRemainsEnemiesText(bool value)
    {
        _remainsEnemiesText.gameObject.SetActive(value);
    }

    public void SetActiveTimeText(bool value)
    {
        _timeWaveText.gameObject.SetActive(value);
    }

    public void SetActiveHUDWindow(bool value)
    {
        _HUDWindow.SetActive(value);
    }

    public void RefreshScore(int count)
    {
        _scoreText.text = GameConstants.UI.HUDText.Score + count;
    }

    public void RefreshAmmo(int curAmmo, int maxClipAmmo)
    {
        _ammoText.text = GameConstants.UI.HUDText.Ammo + curAmmo + GameConstants.UI.HUDText.AmmoSlash + maxClipAmmo;
    }

    public void RefreshHealthPoints(int count)
    {
        _healthText.text = GameConstants.UI.HUDText.PlayerHealth + count;
    }
    public void RefreshTimeWave(float count)
    {
        _timeWaveText.text = GameConstants.UI.HUDText.WaveTime + count.ToString(GameConstants.UI.HUDText.TimeStringFormat);
    }
    public void SetWeaponImage(Sprite image)
    {
        _weaponImage.sprite = image;
    }
    public void SetWave(int value)
    {
        _waveText.text = GameConstants.UI.HUDText.Wave + value;
    }

}
