using UnityEngine;
using UnityEngine.UI;

public class LevelObjectButton : MonoBehaviour
{    
    private LevelObjectPiece _associatedObjectPiece;
    
    [SerializeField]
    private Image _buttonImage;
    [SerializeField]
    private Button _buttonComponent;

    public void SetupButton(LevelObjectPiece setupPiece)
    {
        this._associatedObjectPiece = setupPiece;        
        this._buttonImage.sprite = this._associatedObjectPiece.objectThumbnail;
        this.EnableButton();
    }

    public void SelectLevelObject()
    {        
        PlayerControlsManager.instance.EquipObject(this._associatedObjectPiece.objectPrefab);
        LevelObjectButtonGridManager.instance.UpdateButtonGrid(this);        
    }

    public void EnableButton()
    {
        this._buttonComponent.interactable = true;
    }

    public void DisableButton()
    {
        this._buttonComponent.interactable = false;
    }

}
