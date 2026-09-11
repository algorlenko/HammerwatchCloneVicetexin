using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTriangle : Unit
{
    [SerializeField] PlayerSquare playerUnit;
    [SerializeField] float shootingRadius = 3;
    [SerializeField] float aggroRadius = 6;
    public override void Update()
    {
        isShooting = (playerUnit.transform.position - transform.position).magnitude <= shootingRadius;
        if (!isShooting && (playerUnit.transform.position - transform.position).magnitude <= aggroRadius)
        {
            Move(GetMovementVector());
            isShooting = false;
        }
        else
        {
            Move(Vector2.zero);
        }
        base.Update();
    }

    public Vector2 GetMovementVector()
    {
        return getAimingVector(); //Eventually we can have a more involved and seperate alogrithm for moving and shooting but for now they are the same, and are both just direct to the players position.
    }

    public override Vector2 getAimingVector()
    {
        return (playerUnit.transform.position - this.transform.position).normalized;
    }
}