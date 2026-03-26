using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevelObject : LevelObjectComponent
{

    [Header("Angle Change Per Fixed Update (0.02s)")]
    [SerializeField]
    private float _rotationSpeed;

    public override void SetComponentValues(List<float> values)
    {
        base.SetComponentValues(values);

        this._rotationSpeed = values[0];
    }

    public override List<float> GetComponentValues()
    {
        List<float> values = new List<float>()
        {
            this._rotationSpeed,            
        };

        return values;
    }

    protected override IEnumerator ComponentCoroutine()
    {
        while (true)
        {
            float newRotationAngle = this.objectTransform.rotation.eulerAngles.z - _rotationSpeed * Time.fixedDeltaTime;
            this.objectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
            yield return new WaitForFixedUpdate();
        }
    }
}
