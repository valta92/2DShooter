using UnityEngine;
using System;
using System.Collections;

public class TapEventArgs : EventArgs
{
    public Vector3 position;

    public TapEventArgs(Vector3 _pos)
    {
        position = _pos;
    }
}

public class ColliderEventArgs : EventArgs
{
    public Collider2D collider;

    public ColliderEventArgs(Collider2D col)
    {
        collider = col;
    }
}

public class AxisEventArgs : EventArgs
{
    public Vector2 axis;

    public AxisEventArgs(Vector2 ax)
    {
        axis = ax;
    }
}

public class AttackEventArgs : EventArgs
{
    public GameObject initiator;
    public int damage;

    public AttackEventArgs(GameObject obj , int count)
    {
        initiator = obj;
        damage = count;
    }
}

public class PickupEventArgs : EventArgs
{
    public PickupType type;
    public int count;

    public PickupEventArgs(PickupType t , int c)
    {
        type = t;
        count = c;
    }
}

public class WaveEventArgs : EventArgs
{
    public int lastWave;

    public WaveEventArgs(int wave)
    {
        lastWave = wave;
    }
}

public class UnitArgs : EventArgs
{
    public GameObject unit;

    public UnitArgs(GameObject obj)
    {
        unit = obj;
    }
}