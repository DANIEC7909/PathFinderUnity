using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public int GCost, HCost;
    public bool IsWalkable;
    public Vector3 WorldPosition;

    public Vector2 GridCordinates;

    public Node Parent;

    public Node(bool _isWalkable, Vector3 _worldPos, Vector2 _GridCordinates)
    {
        IsWalkable = _isWalkable;
        WorldPosition = _worldPos;
        GridCordinates = _GridCordinates;
    }

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }

    }
}
