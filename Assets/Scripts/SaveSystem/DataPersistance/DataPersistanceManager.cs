using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class DataPersistanceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistancesObjs;

    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance { get; private set; }

    private void Start()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data persistance manager in the scene");
        }
        instance = this;

        Debug.Log(Application.persistentDataPath + fileName);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistancesObjs = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Laod();

        if (this.gameData == null)
        {
            Debug.Log("Data not found");
            NewGame();
        }

        foreach (var dataPersistanceObj in dataPersistancesObjs)
        {
            dataPersistanceObj.LoadData(gameData);
        }
    }

    private void OnApplicationQuit()
    {
        //DeleteFile();
    }

    public void DeleteFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // check if file exists
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void SaveGame()
    {
        foreach (var dataPersistanceObj in dataPersistancesObjs)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }
    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistances);
    }
}
