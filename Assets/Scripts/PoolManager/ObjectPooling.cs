using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Pool/ObjectPooling")]
public class ObjectPooling 
{
    List<PoolObject> objects;
    Transform objectsParent;

    public void Initialize (int count, PoolObject sample, Transform objects_parent) 
    {
        objects = new List<PoolObject> ();
        objectsParent = objects_parent;
        for (int i=0; i<count; i++) {
            AddObject(sample, objects_parent);
        }
    }

    public void DeactivatePool()
    {
        for (int i=0; i<objects.Count; i++) 
        {
            objects[i].ReturnToPool();
        }
    }
    public PoolObject GetObject () 
    {
        for (int i=0; i<objects.Count; i++) 
        {
            if (objects[i].gameObject.activeInHierarchy==false) 
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent);
        return objects[objects.Count-1];
    }


    void AddObject(PoolObject sample, Transform objects_parent) 
    {
        GameObject temp;
        temp = GameObject.Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent (objects_parent);
        objects.Add(temp.GetComponent<PoolObject> ());
        if (temp.GetComponent<Animator> ())
            temp.GetComponent<Animator> ().StartPlayback ();
        temp.SetActive(false);
    }

} 
