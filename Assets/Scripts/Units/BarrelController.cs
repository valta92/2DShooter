using UnityEngine;
using System;
using System.Collections;

public class BarrelController : MonoBehaviour , IHealth{

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
    public int Damage { get { return _damage; } }
    public float ExplosionRadius { get { return _explosionRadius; } }
    public float ExplosionForce { get { return _explosionForce; } }

    private event EventHandler _onDamaged;
    private event EventHandler _OnDestroyed;

    [SerializeField]private Health _health;
    [SerializeField]private int _damage;
    [SerializeField]private float _explosionRadius;
    [SerializeField]private float _explosionForce;
    [SerializeField]private LayerMask hittableMask;
    [SerializeField]private AudioClip explosionSound;

    private CircleCollider2D _col;
    private Transform _transform;

    void Start () 
    {
        _col = GetComponent<CircleCollider2D>();
        _transform = transform;
    }


    public void TakeDamage(int count)
    {
        _health.healthPoints -= count;

        if (_onDamaged != null)
            _onDamaged(this, null);

        if (_health.healthPoints <= 0)
        {
            Death();
        }
            
        
    }

    public void Death()
    {
        _col.enabled = false;
        Collider2D[] hits = Physics2D.OverlapCircleAll(_transform.position, _explosionRadius, hittableMask);
        _col.enabled = true;

        if(hits.Length != 0)
        {
            /* for(int i = 0; i < hits.Length + 1; i++)
            {
                hits[i].gameObject.SendMessage(GameConstants.FunctionNames.TakeDamage, _damage);
            }*/
        }

        PoolManager.GetObject(GameConstants.Particles.ParticlesNames[1], _transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySingle(explosionSound);
        gameObject.SetActive(false);
        if (_OnDestroyed != null)
            _OnDestroyed(this, null);
    }   
}
