using UnityEngine;
using System.Collections;
using System;


public interface ITriggerArea 
{
    event EventHandler<ColliderEventArgs> OnTriggerEnter;
    event EventHandler<ColliderEventArgs> OnTriggerExit;
    void OnTriggerEnter2D(Collider2D other);
    void OnTriggerExit2D(Collider2D other);
}
