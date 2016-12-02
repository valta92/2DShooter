using UnityEngine;
using System;
using System.Collections;

public class EnemyController : MonoBehaviour , IMove , IHealth 
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
    public float MoveSpeed{ get { return _moveSpeed; } }

    private event EventHandler _onDamaged;
    private event EventHandler _OnDestroyed;

    [SerializeField]private float _moveSpeed;
    [SerializeField]private Health _health;
    [SerializeField]MeleeWeapon _weapon;

    [SerializeField]private float _distanceToTarget;
    [SerializeField]private float _attackTimer = 0;

    public int scoreToGive;
    public int moneyToGive;
    private Transform _transform;
    [SerializeField]private GameObject target;
    void Start()
    {
        
    }
    void OnEnable()
    {
        _health.healthPoints = _health.healthPointsMax;
        target = GameObject.FindGameObjectWithTag("Player");
        _transform = this.transform;
        _health.healthPoints = _health.healthPointsMax;
    }

    void OnDisable()
    {
        
    }
	
	void Update () 
    {
        if (target == null)
            return;
        SimpleAI();
	}

    public void Reset()
    {
        
    }

    private void SimpleAI()
    {
        _attackTimer += Time.deltaTime;
        _distanceToTarget = Vector2.Distance(target.transform.position, _transform.position);
        if(_distanceToTarget > _weapon._attackDistance)
        {
            Move(target.transform.position);
            RotateToTarget(target.transform.position);
        }
        else
        {
            if(_attackTimer >= _weapon.attackRate){
                _attackTimer = 0.0f;
                Attack();
            }
        }
    }

    public void Attack()
    {
        target.SendMessage(GameConstants.FunctionNames.TakeDamage, _weapon.damage);
    }


    public void Move(Vector3 target)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, target, _moveSpeed * Time.deltaTime);
        Vector3 dir = target - transform.position; 
        _transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 90));
    }

    public void RotateToTarget(Vector3 target)
    {
        Vector3 dir = target - _transform.position; 
        _transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 90));
    } 

    public void TakeDamage(int count)
    {
        _health.healthPoints -= count;
        SoundManager.Instance.PlaySingle(_health.hitSound);

        if (_onDamaged != null)
            _onDamaged(this, null);

        if (_health.healthPoints <= 0)
            Death();

    }

    public void Death()
    {
        if (_OnDestroyed != null)
            _OnDestroyed(this, null);

        GameManager.Instance.AddScore(scoreToGive);
        gameObject.SetActive(false);
    }
        


}

[System.Serializable]
public class MeleeWeapon
{
    public float _attackDistance;
    public float attackRate;
    public int damage;
}
