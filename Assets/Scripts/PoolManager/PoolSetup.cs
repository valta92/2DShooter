using UnityEngine;
using System.Collections;

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : MonoBehaviour {//обертка для управления статическим классом PoolManager


    [SerializeField] 
    private PoolManager.PoolGroup[] poolGroups;

    void OnValidate() 
    {
        for(int i = 0; i < poolGroups.Length; i++)
        {
            for (int x = 0; x < poolGroups[i].pools.Length; x++) 
            {
                poolGroups[i].pools[x].name = poolGroups[i].pools[x].prefab.name;
            }
        }
    }

    void Awake() 
    {
        Initialize ();
    }

    void Initialize () 
    {
        PoolManager.Initialize(poolGroups);
    }
}