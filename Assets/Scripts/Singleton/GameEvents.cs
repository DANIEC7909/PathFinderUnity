public class GameEvents : Singleton<GameEvents>
{
   
    private void Awake()
    {
        Init(this);
    }
    #region Delegates
    public delegate void HeroSpawed(Hero spawnedHero);
    public delegate void HeroChanged(Hero changedHero);
    public delegate void SaveGame();
    public delegate void HeroesSpawned();
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
}
