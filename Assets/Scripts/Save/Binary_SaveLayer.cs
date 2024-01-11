
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Just showcase that we can swap between multiple saving layer. It can be expanded to save even to the cloud. 
/// </summary>
public class Binary_SaveLayer : ISaveLayer
{
    string path = Application.persistentDataPath;
    public async void PraseData(SaveData saveData)
    {
        await Task.Run(() =>
        {
            Parallel.Invoke(() =>
            {
                System.IO.Stream ms = File.OpenWrite(path + @"/Save.pfbin");

                BinaryFormatter formatter = new BinaryFormatter();
                //It serialize the employee object  
                formatter.Serialize(ms, saveData);
                ms.Flush();
                ms.Close();
                ms.Dispose();
                GameEvents.Instance.c_OnGameSaved(saveData);
            });
        });
    }

    public SaveData ReadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fs = File.Open(path + @"/Save.pfbin", FileMode.Open);

        object obj = formatter.Deserialize(fs);
        SaveData data = (SaveData)obj;
        fs.Flush();
        fs.Close();
        fs.Dispose();
        GameEvents.Instance.c_OnSaveLoaded(data);
        return data;
    }


}
