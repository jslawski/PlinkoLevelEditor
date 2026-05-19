using UnityEngine;

public class PegHighlight : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _highlightSprite;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private LayerMask _collisionMask;

    public bool isValid = true;

    public void EnableHighlight()
    {
        this._highlightSprite.enabled = true;
    }

    public void DisableHighlight()
    {
        this._highlightSprite.enabled = false;
    }

    public void ActivateValidHighlight()
    {
        this._highlightSprite.color = Color.green;
    }

    public void ActivateInvalidHighlight()
    {
        this._highlightSprite.color = Color.red;
    }

    public void UpdateHighlight()
    {    
        if (Physics.CheckBox(this.gameObject.transform.position, (this.gameObject.transform.localScale / 2.0f), this.gameObject.transform.rotation, this._collisionMask, QueryTriggerInteraction.Collide) == true)
        {
            this.ActivateInvalidHighlight();
            this.isValid = false;
        }
        else
        {
            this.ActivateValidHighlight();
            this.isValid = true;
        }
    }
}
