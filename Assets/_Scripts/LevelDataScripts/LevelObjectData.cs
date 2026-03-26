using System;
using System.Collections.Generic;

[Serializable]
public class LevelObjectData
{
    public string objName;
    public float value;
    public List<float> position;
    public List<float> rotation;
    public List<float> scale;
    public List<LevelObjectComponentData> components;

    public LevelObjectData()
    {
        this.objName = string.Empty;
        this.value = 0.0f;
        this.position = new List<float>();
        this.rotation = new List<float>();
        this.scale = new List<float>();
        this.components = new List<LevelObjectComponentData>();
    }

    public LevelObjectData(LevelObjectData dataToCopy)
    { 
        this.objName= dataToCopy.objName;
        this.value = dataToCopy.value;
        this.position = dataToCopy.position;
        this.rotation= dataToCopy.rotation;
        this.scale = dataToCopy.scale;
        this.components = dataToCopy.components;
    }
}
