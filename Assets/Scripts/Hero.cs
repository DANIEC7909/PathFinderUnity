using UnityEngine;

public class Hero : MonoBehaviour
{
    public HeroObject SO;
    public PathFinder PathFinder;
    public HeroStatistics CurrentHeroStatistics;
    public bool IsPrimaryHero;
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
        PathFinder.Grid = GameControlller.Instance.WorldNavigationGrid;

    }
    public void Move(Vector3 pos)
    {
        PathFinder.FindPath(transform.position, pos);

    }
    private void Update()
    {
        if (PathFinder.Path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, PathFinder.Path[0].WorldPosition, Time.deltaTime * CurrentHeroStatistics.Speed);

            if (PathFinder.Path.Count >= 1 && transform.position == PathFinder.Path[0].WorldPosition )
            {
                PathFinder.Path.RemoveAt(0);
            }
        }
    }
    #endregion
}
