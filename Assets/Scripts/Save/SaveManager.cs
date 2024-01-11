using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        saveData.MapSeed = 123123123;
        saveData.HeroStatistics = new HeroStatisticsSave[GameController.Instance.AllSpawnedHeros.Count];
        for(int i =0; i<GameController.Instance.AllSpawnedHeros.Count;i++)
        {
            saveData.HeroStatistics[i].HeroID = GameController.Instance.AllSpawnedHeros[i].SO.HeroID;
            saveData.HeroStatistics[i].HeroStatistics.Health = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Health;
            saveData.HeroStatistics[i].HeroStatistics.Speed = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Speed;
            saveData.HeroStatistics[i].HeroStatistics.Strength = GameController.Instance.AllSpawnedHeros[i].CurrentHeroStatistics.Strength;
        }
     SaveLayer.PraseData(saveData);
    }
    public void  Load()
    {
      SaveData saveData= SaveLayer.ReadData();
        foreach(Hero h in GameController.Instance.AllSpawnedHeros)
        {
            foreach(HeroStatisticsSave hss in saveData.HeroStatistics)
            {
                if(h.SO.HeroID== hss.HeroID)
                {
                    h.CurrentHeroStatistics.Health = hss.HeroStatistics.Health;
                    h.CurrentHeroStatistics.Strength = hss.HeroStatistics.Strength;
                    h.CurrentHeroStatistics.Speed = hss.HeroStatistics.Speed;
                }
            }
        }
    }
}
