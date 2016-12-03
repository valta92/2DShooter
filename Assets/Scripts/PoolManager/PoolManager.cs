
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class PoolManager
{
    private static GameObject objectsParent;
    private static PoolGroup[] poolGroups;



    [System.Serializable]
    public struct PoolGroup
    {
        public string nameGroup;
        public PoolPart[] pools;

        [System.Serializable]
        public struct PoolPart 
        {
            public string name;
            public PoolObject prefab;
            public int count;
            public ObjectPooling ferula;
        }
    }

    public static void Initialize(PoolGroup[] newGroups) 
    {
        objectsParent = new GameObject();
        objectsParent.name = "Pool";

        poolGroups = newGroups;

        for(int i = 0; i < poolGroups.Length; i++)
        {
            for(int x = 0; x < poolGroups[i].pools.Length; x++)
            {
                if(poolGroups[i].pools[x].prefab != null)
                {
                    poolGroups[i].pools[x].ferula = new ObjectPooling();
                    poolGroups[i].pools[x].ferula.Initialize(poolGroups[i].pools[x].count, poolGroups[i].pools[x].prefab, objectsParent.transform);

                }
            }
        }
    }

    public static GameObject GetObject (string name, Vector3 position, Quaternion rotation) 
    {
        GameObject result = null;
        if (poolGroups != null) 
        {
            for(int i = 0; i < poolGroups.Length; i++)  
            {
                for (int x = 0; x < poolGroups[i].pools.Length; x++) 
                {
                    if (string.Compare (poolGroups[i].pools[x].name, name) == 0) 
                    {
                        result = poolGroups[i].pools[x].ferula.GetObject ().gameObject;
                        result.transform.position = position;
                        result.transform.rotation = rotation;
                        result.SetActive (true);
                        return result;
                    }
                }
            }


        } 
        return result;
    }
        
    public static void DeactivateGroupObjects(string groupName)
    {
        int id = Array.IndexOf(poolGroups, groupName);
        if(id > -1)
        {
            if(poolGroups[id].pools != null)
            {
                for(int i = 0; i < poolGroups[id].pools.Length; i++)
                {
                    poolGroups[id].pools[i].ferula.DeactivatePool();
                }
            }
        }


    }
    public static void DeactivateAll()
    {
        if(poolGroups != null)
        {
            for(int i = 0; i< poolGroups.Length; i++)
            {
                if(poolGroups[i].pools != null)
                {
                    for(int x = 0; i< poolGroups[i].pools.Length; x++)
                    {
                        poolGroups[i].pools[x].ferula.DeactivatePool();
                    }
                }
            } 
        }

    }
}
