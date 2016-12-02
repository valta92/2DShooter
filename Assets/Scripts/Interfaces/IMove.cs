using UnityEngine;
using System.Collections;

public interface IMove
{
    float MoveSpeed { get; }
    void Move(Vector3 target);
}
