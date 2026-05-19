using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject: MonoBehaviour
{
    protected string _prefabName;

    protected float _value;

    [SerializeField]
    protected Transform _transformToScale;

    [HideInInspector]
    public Vector2 minBounds;
    [HideInInspector]
    public Vector2 maxBounds;

    protected Collider[] _allColliders;

    private PegHighlight[] _highlights;

    private void Awake()
    {
        this._highlights = GetComponentsInChildren<PegHighlight>();
    }

    public virtual void LoadLevelObject(LevelObjectData data)
    {
        this._prefabName = data.objName;
        this._value = data.value;
        this.transform.localPosition = new Vector3(data.position[0], data.position[1], data.position[2]);
        this.transform.localRotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);

        if (this._transformToScale != null)
        {
            this._transformToScale.localScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);
        }
        else
        {
            this.transform.localScale = new Vector3(data.scale[0], data.scale[1], data.scale[2]);
        }        

        this.SetupComponents(data.components);
    }

    public void EnablePhysicsCollision()
    { 
        Collider[] allColliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < allColliders.Length; i++)
        {           
            allColliders[i].enabled = true;
            allColliders[i].isTrigger = false;
        }
    }

    public void EnableTriggerCollision()
    {
        Collider[] allColliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            //allColliders[i].enabled = false;
            allColliders[i].isTrigger = true;
        }
    }

    public void DisableCollision()
    {
        Collider[] allColliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            allColliders[i].enabled = false;            
        }
    }

    public void UpdateBounds()
    {
        Vector2 minCandidates = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        Vector2 maxCandidates = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
    
        Collider[] allColliders = GetComponentsInChildren<Collider>();

        for (int i = 0; i < allColliders.Length; i++)
        {
            if (allColliders[i].bounds.min.x < minCandidates.x)
            {
                minCandidates.x = allColliders[i].bounds.min.x;
            }
            if (allColliders[i].bounds.min.y < minCandidates.y)
            {
                minCandidates.y = allColliders[i].bounds.min.y;
            }
            if (allColliders[i].bounds.max.x > maxCandidates.x)
            {
                maxCandidates.x = allColliders[i].bounds.max.x;
            }
            if (allColliders[i].bounds.max.y > maxCandidates.y)
            {
                maxCandidates.y = allColliders[i].bounds.max.y;
            }
        }

        this.minBounds = minCandidates;
        this.maxBounds = maxCandidates;
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

    public void EnableHighlights()
    {
        if (this._highlights != null)
        {
            for (int i = 0; i < this._highlights.Length; i++)
            {
                this._highlights[i].EnableHighlight();
            }
        }
    }

    public void DisableHighlights()
    {
        if (this._highlights != null)
        {
            for (int i = 0; i < this._highlights.Length; i++)
            {
                this._highlights[i].DisableHighlight();
            }
        }
    }

    public void UpdateHighlights()
    {
        if (this._highlights != null)
        {
            for (int i = 0; i < this._highlights.Length; i++)
            {
                this._highlights[i].UpdateHighlight();
            }
        }
    }

    public bool IsValidPosition()
    {
        //Check validity for drop and catch zones
    
        for (int i = 0; i < this._highlights.Length; i++)
        {
            if (this._highlights[i].isValid == false)
            {
                return false;
            }
        }

        return true;
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

        if (this._transformToScale == null)
        {
            scaleData.Add(this.transform.localScale.x);
            scaleData.Add(this.transform.localScale.y);
            scaleData.Add(this.transform.localScale.z);
        }
        else
        {
            scaleData.Add(this._transformToScale.localScale.x);
            scaleData.Add(this._transformToScale.localScale.y);
            scaleData.Add(this._transformToScale.localScale.z);
        }

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
