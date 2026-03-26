using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class OscillateLevelObject : LevelObjectComponent
{
    [Header("Which angles should the object change between?")]
    [SerializeField]
    private float _rotation1 = 0.5f;
    [SerializeField]
    private float _rotation2 = 1.5f;

    [Header("How long should it take to get from Rotation 1 to Rotation 2?")]
    [SerializeField]
    private float _moveTime = 0.5f;

    [Header("How long should the object stay at a target angle before rotating again?")]
    [SerializeField]
    private float _waitTime = 2.0f;

    //private bool _changeToRotation2 = true;

    private Vector3 _rotationVector1;
    private Vector3 _rotationVector2;

    private Sequence _currentlyActiveSequence;

    public override void SetComponentValues(List<float> values)
    {
        base.SetComponentValues(values);

        this._rotation1 = values[0];
        this._rotation2 = values[1];
        this._moveTime = values[2];
        this._waitTime = values[3];

        this._rotationVector1 = new Vector3(0.0f, 0.0f, this._rotation1);
        this._rotationVector2 = new Vector3(0.0f, 0.0f, this._rotation2);
    }

    public override List<float> GetComponentValues()
    {
        List<float> values = new List<float>()
        {
            this._rotation1,
            this._rotation2,
            this._moveTime,
            this._waitTime
        };

        return values;
    }

    public override void StartComponent()
    {
        this.objectTransform.localRotation = Quaternion.Euler(this._rotationVector1);
        this.RotateToRotation2();
    }

    public override void StopComponent()
    {
        this._currentlyActiveSequence.Kill();
    }

    private void RotateToRotation1()
    {
        this._currentlyActiveSequence.Kill();
    
        Sequence rotateSequence = DOTween.Sequence();
        rotateSequence.onComplete -= this.RotateToRotation2;
        rotateSequence.onComplete += this.RotateToRotation2;

        this._currentlyActiveSequence = rotateSequence.Append(this.objectTransform.DOLocalRotate(this._rotationVector1, this._moveTime))
        .AppendInterval(this._waitTime)
        .Play();
    }

    private void RotateToRotation2()
    {
        this._currentlyActiveSequence.Kill();

        Sequence rotateSequence = DOTween.Sequence();
        rotateSequence.onComplete -= this.RotateToRotation1;
        rotateSequence.onComplete += this.RotateToRotation1;

        this._currentlyActiveSequence = rotateSequence.Append(this.objectTransform.DOLocalRotate(this._rotationVector2, this._moveTime))
        .AppendInterval(this._waitTime)
        .Play();
    }

    /*
    protected override IEnumerator ComponentCoroutine()
    {
        this.objectTransform.localRotation = Quaternion.Euler(this._rotationVector1);    

        while (true)
        {
            if (this._changeToRotation2 == true)
            {
                this.objectTransform.DORotate(this._rotationVector2, this._moveTime);
            }
            else
            {
                this.objectTransform.DORotate(this._rotationVector1, this._moveTime);
            }

            this._changeToRotation2 = !this._changeToRotation2;

            yield return new WaitForSeconds(this._moveTime);
        }
    }
    */
}
