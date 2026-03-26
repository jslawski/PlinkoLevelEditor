using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLevelObject : LevelObjectComponent
{
    [Header("Which sizes should the object scale between?")]
    [SerializeField]
    private Vector3 _scale1;
    [SerializeField]
    private Vector3 _scale2;

    [Header("How long should it take to get from Scale 1 to Scale 2?")]
    [SerializeField]
    private float _scaleTime;

    [Header("How long should the object stay at its current size before scaling again?")]
    [SerializeField]
    private float _waitTime;

    //private bool _moveToPosition2 = true;

    private Sequence _currentlyActiveSequence;

    public override void SetComponentValues(List<float> values)
    {
        base.SetComponentValues(values);

        this._scale1 = new Vector3(values[0], values[1], values[2]);
        this._scale2 = new Vector3(values[3], values[4], values[5]);
        this._scaleTime = values[6];
        this._waitTime = values[7];
    }

    public override List<float> GetComponentValues()
    { 
        List<float> values = new List<float>() 
        {
            this._scale1.x, this._scale1.y, this._scale1.z,
            this._scale2.x, this._scale2.y, this._scale2.z,
            this._scaleTime,
            this._waitTime
        };

        return values;
    }

    public override void StartComponent()
    {
        this.objectTransform.localScale = this._scale1;
        this.ScaleToSize2();
    }

    public override void StopComponent()
    {
        this._currentlyActiveSequence.Kill();
    }

    private void ScaleToSize1()
    {
        this._currentlyActiveSequence.Kill();

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.onComplete -= this.ScaleToSize2;
        scaleSequence.onComplete += this.ScaleToSize2;

        this._currentlyActiveSequence = scaleSequence.Append(this.objectTransform.DOMove(this._scale1, this._scaleTime))
        .AppendInterval(this._waitTime)
        .Play();
    }

    private void ScaleToSize2()
    {
        this._currentlyActiveSequence.Kill();

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.onComplete -= this.ScaleToSize1;
        scaleSequence.onComplete += this.ScaleToSize1;

        this._currentlyActiveSequence = scaleSequence.Append(this.objectTransform.DOMove(this._scale2, this._scaleTime))
        .AppendInterval(this._waitTime)
        .Play();
    }
}
