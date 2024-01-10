using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public HeroObject SO;
    public PathFinder PathFinder;
    public HeroStatistics CurrentHeroStatistics;
    public bool IsPrimaryHero;
    public Node CurrentNode;
    [SerializeField] MeshRenderer HeroRenderer;
    #region UnityMethods
    private void OnValidate()
    {
        if (PathFinder == null)
        {
            PathFinder = GetComponent<PathFinder>();
        }
    }
    private void Start()
    {
        PathFinder.Grid = GameController.Instance.WorldNavigationGrid;
        HeroRenderer.material = SO.HeroSkin;
    }
    public void Move(Vector3 pos)
    {
        PathFinder.FindPath(transform.position, pos);

    }
   
    private void Update()
    {
        CurrentNode = PathFinder.Grid.GetNodeFromWorldPosition(transform.position);
        if (IsPrimaryHero)
        {
            if (PathFinder.Path.Count >=1)
            {
                transform.position = Vector3.MoveTowards(transform.position, PathFinder.Path[0].WorldPosition, Time.deltaTime * CurrentHeroStatistics.Speed);

              
                if (PathFinder.Path.Count >= 1 && transform.position == PathFinder.Path[0].WorldPosition)
                {
                    PathFinder.Path.RemoveAt(0);
                }
            }
        }
        else
        {
            if (GameController.Instance.PrimaryHero != null && GameController.Instance.PrimaryHero.PathFinder.Path.Count > 1)
            {
                List<Node> neighbors = GameController.Instance.WorldNavigationGrid.GetNeighbors(GameController.Instance.PrimaryHero.CurrentNode);
                Node selectedNeighborInRangeHero = null;
                foreach (Node n in neighbors)
                {
                    if (n.IsWalkable)
                    {
                        selectedNeighborInRangeHero = n;
                        break;
                    }
                }
                if (selectedNeighborInRangeHero!=null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, selectedNeighborInRangeHero.WorldPosition, Time.deltaTime * CurrentHeroStatistics.Speed);
                }
            }
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (CurrentNode != null)
        {
            Gizmos.DrawCube(CurrentNode.WorldPosition, Vector3.one * .8f);
        }
    }
}
