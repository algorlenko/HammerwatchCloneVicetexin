using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingManager : MonoBehaviour
{
    public static PathFindingManager Instance { get; private set; }

    public Vector2 gridOrigin;

    public int width;
    public int height;

    public float cellSize;

    public LayerMask obstacleMask;

    public bool[,] walkable;

    private static Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
    };


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BuildGrid();
    }

    public void BuildGrid()
    {
        walkable = new bool[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = ToWorld(new Vector2Int(x, y));


                walkable[x, y] = Physics2D.OverlapBox(pos, Vector2.one * cellSize * 0.8f, 0f, obstacleMask) == null;
            }
        }
    }

    Vector2 ToWorld(Vector2Int cell)
    {
        return gridOrigin + new Vector2(
            (cell.x + 0.5f) * cellSize, (cell.y + 0.5f) * cellSize);
    }

    Vector2Int ToCell(Vector2 position)
    {
        Vector2 local = (position - gridOrigin) / cellSize;

        return new Vector2Int(Mathf.FloorToInt(local.x), Mathf.FloorToInt(local.y));
    }

    bool IsValidCell(Vector2Int cell)
    {
        return cell.x >= 0 &&
               cell.y >= 0 &&
               cell.x < width &&
               cell.y < height;
    }

    int Distance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }


    public List<Vector2> FindPath(Vector2 from, Vector2 to)
    {
        List<Vector2> result = new List<Vector2>();

        if (walkable == null)
            return result;

        Vector2Int start = ToCell(from);
        Vector2Int end = ToCell(to);

        if (!IsValidCell(start) || !IsValidCell(end))
            return result;

        if (!walkable[start.x, start.y] || !walkable[end.x, end.y])
            return result;

        List<Vector2Int> openSpots = new List<Vector2Int>();
        HashSet<Vector2Int> closedSpots = new HashSet<Vector2Int>();

        Dictionary<Vector2Int, Vector2Int> parent = new Dictionary<Vector2Int, Vector2Int>();

        Dictionary<Vector2Int, int> gCost = new Dictionary<Vector2Int, int>();

        openSpots.Add(start);
        gCost[start] = 0;

        while (openSpots.Count > 0)
        {
            Vector2Int current = openSpots[0];

            foreach (Vector2Int node in openSpots)
            {
                int f1 = gCost[node] + Distance(node, end);
                int f2 = gCost[current] + Distance(current, end);

                if (f1 < f2)
                    current = node;
            }

            if (current == end)
            {
                while (current != start)
                {
                    result.Add(ToWorld(current));
                    current = parent[current];
                }

                result.Reverse();
                return result;
            }

            openSpots.Remove(current);
            closedSpots.Add(current);

            foreach (Vector2Int direction in directions)
            {
                Vector2Int next = current + direction;

                if (!IsValidCell(next))
                    continue;
                if (!walkable[next.x, next.y] || closedSpots.Contains(next))
                    continue;

                int cost = gCost[current] + 1;

                if (!gCost.ContainsKey(next) || cost < gCost[next])
                {
                    gCost[next] = cost;
                    parent[next] = current;

                    if (!openSpots.Contains(next))
                        openSpots.Add(next);
                }
            }
        }
        return result;
    }
}
