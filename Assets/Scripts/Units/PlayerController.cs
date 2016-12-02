using UnityEngine;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour , IHealth 
{


    public event EventHandler OnDamaged
    {
        add
        {
            _onDamaged += value;
        }
        remove
        {
            _onDamaged -= value;
        }
    }
    public event EventHandler OnDestroyed
    {
        add
        {
            _OnDestroyed += value;
        }
        remove
        {
            _OnDestroyed -= value;
        }
    }

    public Health HealthComponent { get { return _health; } }
    public Weapon CurrentWeaponComponent { get { return _currentWeapon; } }

    public float MoveSpeed{ get { return moveSpeed; } }


    private event EventHandler _onDamaged;
    private event EventHandler _OnDestroyed;
    private event EventHandler<ColliderEventArgs> _onTriggerEnter;
    private event EventHandler<ColliderEventArgs> _onTriggerExit;


    [SerializeField]private float moveSpeed;
    [SerializeField]private Health _health;
    [SerializeField]private Weapon _currentWeapon;
    [SerializeField]private LayerMask _shootableMask;
    private Transform _transform;
    private Vector3 targetPos;
    private Rigidbody2D rb;


    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        _transform = this.transform;
        _health.healthPoints = _health.healthPointsMax;
        GUIManager.Instance.RefreshAmmo(_currentWeapon.ammo, _currentWeapon.ammoInClipMax);
        GUIManager.Instance.RefreshHealthPoints(_health.healthPoints);
        InputManager.Instance.OnMoveAxisChange += (sender, e) => MovementAxis(e.axis);
        InputManager.Instance.OnAimAxisChange += (sender, e) =>  RotateAxis(e.axis);
        InputManager.Instance.OnFireClick += (sender, e) => Shoot(); 
    }
    void OnEnable()
    {


    }

    private void MovementAxis(Vector2 axis)
    {
        rb.velocity = (axis * 2) * moveSpeed;
    }

    public void RotateAxis(Vector3 axis)
    {
        Vector3 newaxis = new Vector3(axis.y, -axis.x, 0);
        var dir = ((transform.position + newaxis))- transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    public void Shoot()
    {
        if (_currentWeapon.isReloaded)
            return;

        if (_currentWeapon.ammo < 1)
        {
            StartCoroutine(Reload());
            return;
        }


        _currentWeapon.ammo--;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _currentWeapon.shootDistance, _shootableMask.value);
        if(hit.collider != null)
        {
            hit.collider.gameObject.SendMessage(GameConstants.FunctionNames.TakeDamage, _currentWeapon.damageWeapon);
        }

        PoolManager.GetObject(GameConstants.Particles.ParticlesNames[2], _currentWeapon.weaponTransform.position, _transform.rotation);
        GUIManager.Instance.RefreshAmmo(_currentWeapon.ammo, _currentWeapon.ammoInClipMax);
        SoundManager.Instance.PlaySingle(_currentWeapon.shootSound);

    }

    private IEnumerator Reload()
    {
        _currentWeapon.isReloaded = true;
        yield return new WaitForSeconds(_currentWeapon.reloadTime);

        if (_currentWeapon.clipsCount > 0)
        {
            _currentWeapon.ammo = _currentWeapon.ammoInClipMax;
            _currentWeapon.clipsCount--;
        }

        GUIManager.Instance.RefreshAmmo(_currentWeapon.ammo, _currentWeapon.ammoInClipMax);
        _currentWeapon.isReloaded = false;

    }

    public void TakeDamage(int count)
    {
        _health.healthPoints -= count;
        SoundManager.Instance.PlaySingle(_health.hitSound);
        GUIManager.Instance.RefreshHealthPoints(_health.healthPoints);

        if (_onDamaged != null)
            _onDamaged(this, null);

        if (_health.healthPoints <= 0)
            Death();

    }

    public void PickupItem(PickupEventArgs args)
    {
        PickupType t = args.type;
        switch (t)
        {
            case PickupType.Ammo:
                {
                    _currentWeapon.maxClips += args.count;
                    break;
                }
            case PickupType.Health:
                {
                    if((_health.healthPoints + args.count) > _health.healthPointsMax)
                    {
                        _health.healthPoints = _health.healthPointsMax;
                    }
                    else
                        _health.healthPoints += args.count;
                    
                    break; 
                }


        }

    }

    public void Death()
    {
        if (_OnDestroyed != null)
            _OnDestroyed(this, null);

        GameObject.Destroy(this.gameObject);
    }
    


}