using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [Tooltip("We just using X and Z")]
    public Vector3 GridWorldSize;
    public float NodeRadius;
    public Node[,] NodeGrid;
    public Vector2Int GridSize;
    public LayerMask ObstacleMask;
    Vector3 worldBottomLeft;
   [SerializeField] float nodeDiameter;

    private void Awake()
    {
        CalculateGridSize();
        CreateGrid();
    }
   
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public void CalculateGridSize()
   {
        nodeDiameter = Mathf.Pow(NodeRadius, 2);

        GridSize = new Vector2Int(Mathf.RoundToInt(GridWorldSize.x/nodeDiameter), Mathf.RoundToInt(GridWorldSize.z / nodeDiameter));
   }

   public void CreateGrid()
   {
        NodeGrid = new Node[GridSize.x, GridSize.y];
        
        worldBottomLeft = transform.position - ((Vector3.right * GridWorldSize.x / 2) + (Vector3.forward * GridWorldSize.z / 2));
   
        for(int x = 0;x<GridSize.x;x++)
        {
            for(int y = 0; y < GridSize.y;y++)
            {
                Vector3 calculatedWorldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
               // Vector3 worldPoint = new Vector3(calculatedWorldPoint.x,0, calculatedWorldPoint.y);

                bool isWalkable = Physics.OverlapSphere(calculatedWorldPoint, NodeRadius, ObstacleMask).Length > 0 ? false : true;
              
                NodeGrid[x, y] = new Node(isWalkable, calculatedWorldPoint, new Vector2Int(x, y));
            }
        }
    
    }

   public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        return null;
    }
   public List<Node> GetNeighbors(Node node)
    {
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, .92f, .016f, .3f);

        Gizmos.DrawCube(transform.position + Vector3.up*.05f, GridWorldSize);

        Gizmos.color = Color.red;
        if (NodeGrid!=null && NodeGrid.Length > 0)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    if (NodeGrid[x, y].IsWalkable)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(NodeGrid[x, y].WorldPosition, Vector3.one * (NodeRadius - .2f));
                    }
                    else
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(NodeGrid[x, y].WorldPosition, Vector3.one * (NodeRadius - .2f));
                    }
                }
            }
        }
    }
}
