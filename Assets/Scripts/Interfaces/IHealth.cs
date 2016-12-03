using UnityEngine;
using System.Collections;
using System;

public interface IHealth
{
    event EventHandler<UnitArgs> OnDamaged;
    event EventHandler<UnitArgs> OnDestroyed;

    Health HealthComponent { get; }

    void TakeDamage(int count);
    void Death();
	
}
