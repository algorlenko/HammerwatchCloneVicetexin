using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSquare : Unit
{
    [SerializeField] Camera myCamera;
    public override Vector2 getAimingVector()
    {
        return (myCamera.ScreenToWorldPoint(Input.mousePosition) - this.transform.position).normalized;
    }

}
