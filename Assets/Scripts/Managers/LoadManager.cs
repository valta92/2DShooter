using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadManager : PersistentSingleton<LoadManager> {

    public override void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        base.Awake();
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
