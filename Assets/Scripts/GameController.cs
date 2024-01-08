using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public List<Hero> AllSpawnedHeros;
    public Hero PrimaryHero;

    public Grid WorldNavigationGrid; 

    #region UnityMethods
    private void Awake()
    {
        Init(this);
    }
    void Start()
    {
        

    }
    void Update()
    {

    }
    #endregion
    /// <summary>
    /// Spawn hero by it's PrefabKey
    /// </summary>
    /// <param name="key"></param>
    public void SpawnHero(string key)
    {

    }
    /// <summary>
    /// Select hero by argument.
    /// </summary>
    /// <param name="heroToSelect"></param>
    public void SelectHero(Hero heroToSelect)
    {
        if (AllSpawnedHeros.Contains(heroToSelect))
        {
            if (PrimaryHero != null)
            {
                PrimaryHero.IsPrimaryHero = false;
            }
            PrimaryHero = heroToSelect;
            PrimaryHero.IsPrimaryHero = true;
        }
        else
        {
            #region DEBUG 
            Debug.LogError($"Hero not containd in {nameof(AllSpawnedHeros)} registry.");
            #endregion
        }
    }
}
