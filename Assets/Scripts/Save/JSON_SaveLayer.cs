using System.IO;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Just showcase that we can swap between multiple saving layer. It can be expanded to save even to the cloud. 
/// </summary>
public class JSON_SaveLayer : ISaveLayer
{
    public async void PraseData(SaveData saveData)
    {
        string JSONData = JsonUtility.ToJson(saveData);
       
      await  File.WriteAllTextAsync(Application.persistentDataPath + @"/Save.json", JSONData);
        GameEvents.Instance.c_OnGameSaved(saveData);
    }

    public SaveData ReadData()
    {
        string JSONData = File.ReadAllText(Application.persistentDataPath + @"/Save.json");
     SaveData data = JsonUtility.FromJson<SaveData>(JSONData);
        GameEvents.Instance.c_OnSaveLoaded(data);
        return data;
    }

}
