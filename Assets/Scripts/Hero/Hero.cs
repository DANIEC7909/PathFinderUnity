using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class Hero : MonoBehaviour
{
    public HeroObject SO;
    public PathFinder PathFinder;
    public HeroStatistics CurrentHeroStatistics;
    public bool IsPrimaryHero;
    [SerializeField] MeshRenderer HeroRenderer;
    public Node CurrentNode;
    Node LastPrimaryHeroNode;
    [SerializeField] TextMeshPro statsDisplay;

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
        PathFinder.Grid = Instantiate(GameController.Instance.WorldNavigationGrid);
        HeroRenderer.material = SO.HeroSkin;
        if (statsDisplay)
            statsDisplay.color = Random.ColorHSV();
    }



    public void Move(Vector3 pos)
    {
        FindPathAsync(transform.position,pos);
        GameEvents.Instance.c_OnPrimaryHeroPathFound(PathFinder.Path.ToArray());
    }

    private void Update()
    {
        if (!PathFinder.Grid)
        {
#if DEBUG
            Debug.LogError("Grid Of Pathfinder is null");
#endif
            return;
        }
            CurrentNode = PathFinder.Grid.GetNodeFromWorldPosition(transform.position);
         if (PathFinder.Path.Count >= 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, PathFinder.Path[0].WorldPosition, Time.deltaTime * CurrentHeroStatistics.Speed);


            if (PathFinder.Path.Count >= 1 && transform.position == PathFinder.Path[0].WorldPosition)
            {
                PathFinder.Path.RemoveAt(0);
            }
        }
        if (!IsPrimaryHero)
        {
            if (GameController.Instance.PrimaryHero.CurrentNode != LastPrimaryHeroNode)
            {
                if (GameController.Instance.PrimaryHero != null)
                {
                    CalcPathAsync(transform.position);
                }
            }
        }
        if (statsDisplay)
            statsDisplay.text = "Speed=" + CurrentHeroStatistics.Speed.ToString() + System.Environment.NewLine + "Strength =" + CurrentHeroStatistics.Strength.ToString() + System.Environment.NewLine + "Health =" + CurrentHeroStatistics.Health.ToString();

    }
    public async void FindPathAsync(Vector3 CurrentPos, Vector3 TargetPos)
    {
        await Task.Run(() =>
        {
            Parallel.Invoke(() =>
            {
             PathFinder.FindPath(CurrentPos, TargetPos);
            });
        });
    }
    public async void CalcPathAsync(Vector3 pos)
    {
        await Task.Run(() =>
        {
            Parallel.Invoke(() =>
            {
                //converting Node from Primary HeroGrid to this Hero.
                Node[] neighbors = PathFinder.Grid.GetNeighbors(PathFinder.Grid.GetNodeFromWorldPosition(GameController.Instance.PrimaryHero.CurrentNode.WorldPosition)).ToArray();
              
                System.Random rand = new System.Random();

                Node selectedNeighborInRangeHero = neighbors[rand.Next(0, neighbors.Length - 1)];
              FindPathAsync(pos, selectedNeighborInRangeHero.WorldPosition);
                LastPrimaryHeroNode = GameController.Instance.PrimaryHero.CurrentNode;
            });
        });
    }
    private void OnDestroy()
    {
        if (PathFinder.Grid != null)
        {
            Destroy(PathFinder.Grid.gameObject);
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
