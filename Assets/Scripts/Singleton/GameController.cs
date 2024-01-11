using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public Grid WorldNavigationGrid;

    public HeroObject[] SpawnHeroes;
    public List<Hero> AllSpawnedHeros;
    public Hero PrimaryHero;

    private AsyncOperationHandle<GameObject> HeroAssetHandle;
    private GameObject HeroToSpawnTemplate;
    public UIHeroPanel UIHeroPanel;
    public int MapSeed;
    #region UnityMethods
    private void Awake()
    {
        Init(this);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    void Start()
    {
        foreach (HeroObject ho in SpawnHeroes)
        {
            SpawnHero(ho);
        }
    }
    
    private void OnDestroy()
    {
        Addressables.Release(HeroAssetHandle);
    }
    #endregion
    private void InstantiateAndSetupHero(HeroObject heroObject)
    {
        Hero hero = Instantiate(HeroToSpawnTemplate).GetComponent<Hero>();
        if (!hero)
        {
#if DEBUG
            Debug.LogError($"Loaded Asset cannot be converted to Hero");
#endif
            return;
        }
        AllSpawnedHeros.Add(hero);
        hero.SO = heroObject;
       
        //calculate and set hero values, when is spawned.
        hero.CurrentHeroStatistics = new HeroStatistics
        {
            Speed =Random.Range(1,hero.SO.HeroMaxStatistics.Speed),
            Strength= Random.Range(1, hero.SO.HeroMaxStatistics.Strength),
            Health= Random.Range(1, hero.SO.HeroMaxStatistics.Health)
        };

        if (hero.SO == SpawnHeroes[0])
        {
            SelectHero(hero);
        }
      
        GameEvents.Instance.c_OnHeroSpawned(hero);
        
        if (AllSpawnedHeros.Count == SpawnHeroes.Length)
        {
            GameEvents.Instance.c_AllHeroesSpawned();
        }

    }
    /// <summary>
    /// Spawn hero by it's PrefabKey
    /// </summary>
    /// <param name="hero"></param>
    public void SpawnHero(HeroObject heroObject)
    {
        if (!HeroToSpawnTemplate)
        {
            HeroAssetHandle = Addressables.LoadAssetAsync<GameObject>(heroObject.PrefabKey);
            HeroAssetHandle.WaitForCompletion();
            HeroToSpawnTemplate = HeroAssetHandle.Result;
        }
        InstantiateAndSetupHero(heroObject);
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
            GameEvents.Instance.c_OnHeroChanged(PrimaryHero);
        }
        else
        {
            #region DEBUG 
            Debug.LogError($"Hero not containd in {nameof(AllSpawnedHeros)} registry.");
            #endregion
        }
    }
}
