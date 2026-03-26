using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectComponent : MonoBehaviour
{
    protected List<float> componentValues;
    protected Transform objectTransform;

    public virtual void SetComponentValues(List<float> values)
    { 
        this.componentValues = values;
        this.objectTransform = this.gameObject.GetComponent<Transform>();
    }

    public virtual List<float> GetComponentValues()
    {
        return this.componentValues;
    }

    public LevelObjectComponentData GetLevelObjectComponentData()
    {
        LevelObjectComponentData componentData = new LevelObjectComponentData();
        componentData.compName = this.name;
        componentData.values = this.GetComponentValues();

        return componentData;
    }

    public virtual void StartComponent()
    {
        StartCoroutine(this.ComponentCoroutine());
    }

    public virtual void StopComponent()
    {
        StopAllCoroutines();
    }

    protected virtual IEnumerator ComponentCoroutine()
    {
        yield return null;
    }
}
