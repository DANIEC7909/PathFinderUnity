using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JSON_SaveLayer : ISaveLayer
{
    public async void PraseData(SaveData saveData)
    {
        string JSONData = JsonUtility.ToJson(saveData);
        Debug.Log(JSONData + " " + Application.persistentDataPath);

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
