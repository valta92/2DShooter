using UnityEngine;


[System.Serializable]
public class Weapon
{
    public int ammo;
    public  int ammoInClipMax;
    public int clipsCount;
    public int maxClips;
    public float shootDistance;
    public int damageWeapon;
    public bool isReloaded;
    public float reloadTime;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public Transform weaponTransform;
}
