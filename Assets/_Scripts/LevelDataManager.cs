using System.IO;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager instance;

    [SerializeField]
    private Transform _levelParentTransform;

    private LevelData _levelData;

    public bool creatorMode = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //this.LoadLevelData("TestLevel.json");
        this.SaveLevelData();
    }

    public void SaveLevelData()
    {
        this._levelData = new LevelData();

        LevelObject[] allLevelObjects = this._levelParentTransform.gameObject.GetComponentsInChildren<LevelObject>();

        for (int i = 0; i < allLevelObjects.Length; i++)
        {
            this._levelData.lvlObjects.Add(allLevelObjects[i].GetLevelObjectData());
        }

        this.WriteLevelToFile();
    }

    private void WriteLevelToFile()
    { 
        string levelDataJson = JsonUtility.ToJson(this._levelData);

        File.WriteAllText(Application.dataPath + "/SavedLevel.json", levelDataJson);
    }

    public void LoadLevelData(string levelFileName)
    {
        string levelDataJson = File.ReadAllText(Application.dataPath + "\\" + levelFileName);

        this._levelData = JsonUtility.FromJson<LevelData>(levelDataJson);

        this.BuildLoadedLevel();
    }

    private void BuildLoadedLevel()
    {
        for (int i = 0; i < this._levelData.lvlObjects.Count; i++)
        { 
            this.LoadLevelObject(this._levelData.lvlObjects[i]);
        }
    }

    private void LoadLevelObject(LevelObjectData lvlObjectData)
    {
        GameObject objectPrefab = Resources.Load<GameObject>("PlinkoPrefabs/" + lvlObjectData.objName);
        GameObject objectInstance = Instantiate(objectPrefab, this._levelParentTransform);
        
        LevelObject levelObject = objectInstance.GetComponent<LevelObject>();
        
        levelObject.LoadLevelObject(lvlObjectData);
        levelObject.StartComponents();
    }
}
