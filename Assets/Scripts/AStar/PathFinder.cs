using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Grid Grid;
    public Node SeekerNode;
    public Node TargetNode;
    public List<Node> Path = new List<Node>();



    public Node selection()
    {
        return null;
    }

    public void FindPath(Vector3 SeekerPos, Vector3 TargetPos)
    {
        //get player and target position in grid coords
        SeekerNode = Grid.GetNodeFromWorldPosition(SeekerPos);
        TargetNode = Grid.GetNodeFromWorldPosition(TargetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(SeekerNode);
        //calculates path for pathfinding
        while (openSet.Count > 0)
        {

            //iterates through openSet and finds lowest FCost
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost <= node.FCost)
                {
                    if (openSet[i].HCost < node.HCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            //If target found, retrace path
            if (node == TargetNode)
            {
                RetracePath(SeekerNode, TargetNode);
                return;
            }

            foreach (Node neighbour in Grid.GetNeighbors(node))
            {
                if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.GCost + CalculateNodeDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = CalculateNodeDistance(neighbour, TargetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }



            // Profiler.EndSample();
        }
        //   Profiler.EndSample();
        return;
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> retracedPath = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            retracedPath.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        retracedPath.Reverse();

        Path = retracedPath;

    }
    public int CalculateNodeDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridCordinates.x - nodeB.GridCordinates.x);
        int dstY = Mathf.Abs(nodeA.GridCordinates.y - nodeB.GridCordinates.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
