using System;
using System.Collections.Generic;

[Serializable]
public class LevelObjectComponentData
{    
    public string compName;    
    public List<float> values;

    public LevelObjectComponentData()
    {
        this.compName = string.Empty;
        this.values = new List<float>();
    }

    public LevelObjectComponentData(LevelObjectComponentData dataToCopy)
    { 
        this.compName= dataToCopy.compName;
        this.values = dataToCopy.values;
    }
}
