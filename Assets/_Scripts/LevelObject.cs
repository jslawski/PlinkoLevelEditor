using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject: MonoBehaviour
{
    protected string _prefabName;

    protected float _value;    

    public virtual void LoadLevelObject(LevelObjectData data)
    {
        this._prefabName = data.objName;
        this._value = data.value;
        this.transform.localPosition = new Vector3(data.position[0], data.position[1], data.position[2]);
        this.transform.localRotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);
        this.transform.localScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);

        this.SetupComponents(data.components);
    }

    public void StartComponents()
    {
        LevelObjectComponent[] allComponents = GetComponents<LevelObjectComponent>();
        for (int i = 0; i < allComponents.Length; i++)
        {
            allComponents[i].StartComponent();
        }
    }

    public void StopComponents()
    {
        LevelObjectComponent[] allComponents = GetComponents<LevelObjectComponent>();
        for (int i = 0; i < allComponents.Length; i++)
        {
            allComponents[i].StopComponent();
        }
    }

    public LevelObjectData GetLevelObjectData()
    { 
        LevelObjectData levelObjectData = new LevelObjectData();

        levelObjectData.objName = this._prefabName;
        levelObjectData.value = this._value;
        levelObjectData.position = this.GetPositionData();
        levelObjectData.rotation = this.GetRotationData();
        levelObjectData.scale = this.GetScaleData();        
        levelObjectData.components = this.GetObjectComponentsData();

        return levelObjectData;
    }

    private List<float> GetPositionData()
    { 
        List<float> positionData = new List<float>();
        positionData.Add(this.transform.localPosition.x);
        positionData.Add(this.transform.localPosition.y);
        positionData.Add(this.transform.localPosition.z);

        return positionData;
    }

    private List<float> GetRotationData()
    {
        List<float> rotationData = new List<float>();
        rotationData.Add(this.transform.localRotation.x);
        rotationData.Add(this.transform.localRotation.y);
        rotationData.Add(this.transform.localRotation.z);

        return rotationData;
    }
    private List<float> GetScaleData()
    {
        List<float> scaleData = new List<float>();
        scaleData.Add(this.transform.localScale.x);
        scaleData.Add(this.transform.localScale.y);
        scaleData.Add(this.transform.localScale.z);

        return scaleData;
    }

    private List<LevelObjectComponentData> GetObjectComponentsData()
    {
        List<LevelObjectComponentData> allComponentsData = new List<LevelObjectComponentData>();    

        LevelObjectComponent[] allComponents = GetComponents<LevelObjectComponent>();

        for (int i = 0; i < allComponents.Length; i++) 
        {
            LevelObjectComponentData newData = allComponents[i].GetLevelObjectComponentData();            
            allComponentsData.Add(newData);
        }

        return allComponentsData;
    }

    private void SetupComponents(List<LevelObjectComponentData> allComponents)
    {
        for (int i = 0; i < allComponents.Count; i++)
        {
            Type componentType = Type.GetType(allComponents[i].compName);

            LevelObjectComponent newComponent = this.gameObject.AddComponent(componentType) as LevelObjectComponent;
            newComponent.SetComponentValues(allComponents[i].values);
        }
    }
}
