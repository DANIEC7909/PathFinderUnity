public class GameEvents : Singleton<GameEvents>
{
   
    private void Awake()
    {
        Init(this);
    }
    #region Delegates
    public delegate void HeroSpawed(Hero spawnedHero);
    public delegate void HeroChanged(Hero changedHero);
    public delegate void HeroesSpawned();
    
    public delegate void SaveGame(SaveData saveData);
    public delegate void SaveLoaded(SaveData saveData);

    #endregion
    public event HeroesSpawned OnAllHeroesSpawned;
    public void c_AllHeroesSpawned()
    {
        OnAllHeroesSpawned?.Invoke();
    }

    public event HeroSpawed OnHeroSpawned;
    public void c_OnHeroSpawned(Hero spawnedHero)
    {
        OnHeroSpawned?.Invoke(spawnedHero);
    }

    public event HeroChanged OnHeroChanged;
    public void c_OnHeroChanged(Hero changedHero)
    {
        OnHeroChanged?.Invoke(changedHero);
    }
    public event SaveGame OnGameSaved;
    public void c_OnGameSaved(SaveData saveData)
    {
        OnGameSaved?.Invoke(saveData);
    }
    public event SaveLoaded OnSaveLoaded;
    public void c_OnSaveLoaded(SaveData saveData)
    {
        OnSaveLoaded?.Invoke(saveData);
    }
}
