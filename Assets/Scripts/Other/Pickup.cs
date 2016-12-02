using UnityEngine;
using System;
using System.Collections;

public class Pickup : MonoBehaviour , ITriggerArea  {
    

    public event EventHandler<ColliderEventArgs> OnTriggerExit
    {
        add
        {
            _onTriggerExited += value;
        }
        remove
        {
            _onTriggerExited -= value;
        }
    }
    public event EventHandler<ColliderEventArgs> OnTriggerEnter
    {
        add
        {
            _onTriggerEntered+= value;
        }
        remove
        {
            _onTriggerEntered -= value;
        }
    }
    private event EventHandler<ColliderEventArgs>_onTriggerExited;
    private event EventHandler<ColliderEventArgs> _onTriggerEntered;

	public PickupType type;
    public int count;
    public AudioClip pickupSound;
	

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("PickupItem", new PickupEventArgs(type, count));
            SoundManager.Instance.PlaySingle(pickupSound);
            gameObject.SetActive(false);

        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        // 
    }
}


public enum PickupType {
    Health,
    Ammo,
}