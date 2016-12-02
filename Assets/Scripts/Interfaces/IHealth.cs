using UnityEngine;
using System.Collections;
using System;

public interface IHealth
{
    event EventHandler OnDamaged;
    event EventHandler OnDestroyed;

    Health HealthComponent { get; }

    void TakeDamage(int count);
    void Death();
	
}
