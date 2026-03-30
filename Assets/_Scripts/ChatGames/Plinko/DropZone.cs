using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropZone : LevelObject
{
    [SerializeField]
    private BoxCollider _collider;

    [SerializeField]
    private TextMeshProUGUI _commandText;

    [SerializeField]
    private GameObject _plinkoBallPrefab;


    public override void LoadLevelObject(LevelObjectData data)
    {
        base.LoadLevelObject(data);
        
        this._commandText.text = "!" + this._value.ToString();
    }

    public void DropBall()
    {
        float minX = this._collider.transform.position.x - (this._collider.bounds.size.x / 2.0f);
        float maxX = this._collider.transform.position.x + (this._collider.bounds.size.x / 2.0f);
        float minY = this._collider.transform.position.y - (this._collider.bounds.size.y / 2.0f);
        float maxY = this._collider.transform.position.y + (this._collider.bounds.size.y / 2.0f);

        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        Vector3 instantiationPosition = new Vector3(randX, randY, -0.1f);
        GameObject ballInstance = Instantiate(this._plinkoBallPrefab, instantiationPosition, new Quaternion());
        ballInstance.layer = LayerMask.NameToLayer("launchedCabbage");                
    }
}
