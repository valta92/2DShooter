using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	void Start () 
    {
        GameManager.Instance.Initialize();
        WaveManager.Instance.Initialize();
        InputManager.Instance.Initialize();
        SoundManager.Instance.Initialize();


        GameManager.Instance.StartGame();
	}
}
