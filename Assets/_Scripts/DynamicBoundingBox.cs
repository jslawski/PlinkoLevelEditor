using UnityEngine;

public class DynamicBoundingBox : MonoBehaviour
{
    [SerializeField]
    private Transform _visualsTransform;
    [SerializeField]
    private SpriteRenderer _visualsSprite;
    [SerializeField]
    private Collider _collider;

    public bool isColliding = false;

    public void Setup(Vector2 minBounds, Vector2 maxBounds)
    {
        float xScale = (maxBounds.x - minBounds.x);
        float yScale = (maxBounds.y - minBounds.y);

        this._visualsTransform.localScale = new Vector3(xScale, yScale, 1.0f);
        this._collider.transform.localScale = new Vector3(xScale, yScale, 1.0f);
    }

    public void FixedUpdate()
    {
        Debug.LogError("Derp");
    
        if (Physics.CheckBox(this.gameObject.transform.position, this._collider.bounds.extents, this.gameObject.transform.rotation) == true)
        {
            this.isColliding = true;
            this._visualsSprite.color = Color.red;
        }
        else
        {
            this.isColliding = false;
            this._visualsSprite.color = Color.green;
        }        
    }
}
