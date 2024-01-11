public class SaveManager : Singleton<SaveManager>
{
    public ISaveLayer SaveLayer;

    public void Awake()
    {
        Init(this);
        SaveLayer = new JSON_SaveLayer();
    }

    public void Save()
    {
        SaveData saveData = new SaveData();
        saveData.MapSeed = GameController.Instance.MapSeed;
        saveData.HeroStatistics = new HeroStatisticsSave[GameController.Instance.AllSpawnedHeros.Count];
        for (int i = 0; i < GameController.Instance.AllSpawnedHeros.Count; i++)
        {
            saveData.HeroStatistics[i].HeroID = GameController.Instance.AllSpawnedHeros[i].SO.HeroID;
            saveData.HeroStatistics[i].HeroStatistics.Health = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Health;
            saveData.HeroStatistics[i].HeroStatistics.Speed = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Speed;
            saveData.HeroStatistics[i].HeroStatistics.Strength = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Strength;
        }
        SaveLayer.PraseData(saveData);
    }
    public void Load()
    {
        SaveData saveData = SaveLayer.ReadData();
        GameController.Instance.MapSeed = saveData.MapSeed;

        for (int i = 0; i < GameController.Instance.AllSpawnedHeros.Count; i++)
        {
            GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Health = saveData.HeroStatistics[i].HeroStatistics.Health;
            GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Speed = saveData.HeroStatistics[i].HeroStatistics.Speed;
            GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Strength = saveData.HeroStatistics[i].HeroStatistics.Strength;
        }
    }
}
