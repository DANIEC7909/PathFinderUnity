
using UnityEngine;

[System.Serializable]
public class Node 
{
    public int GCost, HCost;
    public bool IsWalkable;
    public Vector3 WorldPosition;

    public Vector2Int GridCordinates;

    public Node Parent;

    public Node(bool _isWalkable, Vector3 _worldPos, Vector2Int _GridCordinates)
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
