using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTriangle : Unit
{
    [SerializeField] PlayerSquare playerUnit;
    [SerializeField] float aggroRadius = 3;
    public override void Update()
    {
        isShooting = (playerUnit.transform.position - transform.position).magnitude <= aggroRadius;
        base.Update();
    }

    public override Vector2 getAimingVector()
    {
        return (playerUnit.transform.position - this.transform.position).normalized;
    }
}