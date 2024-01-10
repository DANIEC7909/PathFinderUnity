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
    int currentHeroIndex;
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
        currentHeroIndex = GameController.Instance.AllSpawnedHeros.IndexOf(this);
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
                Node selectedNeighborInRangeHero = neighbors[currentHeroIndex];

                if (!selectedNeighborInRangeHero.IsWalkable)
                {
                    int nIndex=default;
                    while (!selectedNeighborInRangeHero.IsWalkable)
                    {
                        selectedNeighborInRangeHero = neighbors[nIndex];
                        nIndex++;
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
