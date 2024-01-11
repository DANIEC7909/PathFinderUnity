using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JSON_SaveLayer : ISaveLayer
{
    public async void PraseData(SaveData saveData)
    {
        string JSONData = JsonUtility.ToJson(saveData);
        Debug.Log(JSONData + " " + Application.persistentDataPath);
        GameEvents.Instance.c_OnGameSaved(saveData);

      await  File.WriteAllTextAsync(Application.persistentDataPath + @"/Save.json", JSONData);
    }

    public SaveData ReadData()
    {
        string JSONData = File.ReadAllText(Application.persistentDataPath + @"/Save.json");
        return JsonUtility.FromJson<SaveData>(JSONData);
    }

}
