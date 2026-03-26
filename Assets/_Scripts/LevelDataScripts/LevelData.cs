using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{    
    public List<LevelObjectData> lvlObjects;

    public LevelData()
    {
        this.lvlObjects = new List<LevelObjectData>();
    }

    public LevelData(LevelData dataToCopy)
    { 
        this.lvlObjects = dataToCopy.lvlObjects;
    }
}
