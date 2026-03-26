using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveLevelObject : LevelObjectComponent
{
    [Header("Which positions should the object move between?")]
    [SerializeField]
    private Vector3 _position1;
    [SerializeField]
    private Vector3 _position2;

    [Header("How long should it take to get from Position 1 to Position 2?")]
    [SerializeField]
    private float _moveTime;

    [Header("How long should the object stay at a destination before moving again?")]
    [SerializeField]
    private float _waitTime;

    //private bool _moveToPosition2 = true;

    private Sequence _currentlyActiveSequence;

    public override void SetComponentValues(List<float> values)
    {
        base.SetComponentValues(values);

        this._position1 = new Vector3(values[0], values[1], values[2]);
        this._position2 = new Vector3(values[3], values[4], values[5]);
        this._moveTime = values[6];
        this._waitTime = values[7];
    }

    public override List<float> GetComponentValues()
    {
        List<float> values = new List<float>()
        {
            this._position1.x, this._position1.y, this._position1.z,
            this._position2.x, this._position2.y, this._position2.z,
            this._moveTime,
            this._waitTime
        };

        return values;
    }

    public override void StartComponent()
    {
        this.objectTransform.position = this._position1;

        this.MoveToPosition2();
    }

    public override void StopComponent()
    {
        this._currentlyActiveSequence.Kill();
    }

    private void MoveToPosition1()
    {
        this._currentlyActiveSequence.Kill();

        Sequence moveSequence = DOTween.Sequence();
        moveSequence.onComplete -= this.MoveToPosition2;
        moveSequence.onComplete += this.MoveToPosition2;

        this._currentlyActiveSequence = moveSequence.Append(this.objectTransform.DOMove(this._position1, this._moveTime))
        .AppendInterval(this._waitTime)
        .Play();
    }

    private void MoveToPosition2()
    {
        this._currentlyActiveSequence.Kill();

        Sequence moveSequence = DOTween.Sequence();
        moveSequence.onComplete -= this.MoveToPosition1;
        moveSequence.onComplete += this.MoveToPosition1;

        this._currentlyActiveSequence = moveSequence.Append(this.objectTransform.DOMove(this._position2, this._moveTime))
        .AppendInterval(this._waitTime)
        .Play();
    }

    /*
    protected override IEnumerator ComponentCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(this._waitTime);

            if (this._moveToPosition2 == true)
            {
                this.objectTransform.DOMove(this._position2, this._moveTime);
            }
            else
            {
                this.objectTransform.DOMove(this._position1, this._moveTime);
            }

            this._moveToPosition2 = !this._moveToPosition2;
        }
    }
    */

}
