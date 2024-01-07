using System.Collections;
using System.Collections.Generic;
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
    private void Update()
    {
       
    }
    #endregion
}
