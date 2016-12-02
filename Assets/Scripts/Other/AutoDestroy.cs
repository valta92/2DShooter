using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {


    [SerializeField]private float _timeToDestroy;

	void OnEnable () 
    {
        Invoke(GameConstants.FunctionNames.DestroySelf, _timeToDestroy);
	}

    public void DestroySelf()
    {
        this.gameObject.SetActive(false);
    }
}
