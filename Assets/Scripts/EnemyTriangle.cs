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
    List<Vector2> currentPath = new List<Vector2>();
    int currentIndex = 0;
    float timeLeftTillNextPathfind = 0.5f;
    float pathFindTimeInterval = 0.5f;
    [SerializeField] float pathFindDistanceMargin = 0.25f;
    public override void Update()
    {
        isShooting = (playerUnit.transform.position - transform.position).magnitude <= shootingRadius;
        timeLeftTillNextPathfind += Time.deltaTime;

        if (!isShooting && (playerUnit.transform.position - transform.position).magnitude <= aggroRadius)
        {
            if (timeLeftTillNextPathfind >= pathFindTimeInterval) { RefreshPathFinding(); }
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

        if (currentPath == null || currentPath.Count == 0) { return Vector2.zero; }
        if (currentIndex < currentPath.Count - 1 && Vector2.Distance(new Vector2(transform.position.x, transform.position.y), currentPath[currentIndex]) < pathFindDistanceMargin)
        {
            currentIndex += 1;
        }
         return (currentPath[currentIndex] - new Vector2(transform.position.x, transform.position.y)).normalized; 
    }

    void RefreshPathFinding()
    {
        currentIndex = 0;
        currentPath = PathFindingManager.Instance.FindPath(new Vector2(transform.position.x, transform.position.y), new Vector2(playerUnit.transform.position.x, playerUnit.transform.position.y));
        timeLeftTillNextPathfind = 0;
    }

    public override Vector2 getAimingVector()
    {
        return (playerUnit.transform.position - transform.position).normalized;
    }
}