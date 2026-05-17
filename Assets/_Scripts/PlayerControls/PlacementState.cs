using UnityEngine;

public class PlacementState : PlayerControlState
{
    private GameObject _placeableObject;

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        if (PlayerControlsManager.instance.equippedLevelObjectInstance == null)
        {
            return;
        }

        this.UpdateObjectPosition();

        //Continuously update equipped object state, and check to see if it can be placed in a location

        //If LMB and valid position, then place the object
        if (Input.GetMouseButtonDown(0) == true && this.IsValidPosition())
        {
            PlayerControlsManager.instance.PlaceEquippedObject(this.GetMouseWorldPosition());
        }
    }

    public override void ExitState()
    {

    }

    private void UpdateObjectPosition()
    {        
        PlayerControlsManager.instance.equippedLevelObjectInstance.transform.position = this.GetMouseWorldPosition();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePositionScreenSpace = Input.mousePosition;
        mousePositionScreenSpace.z = Camera.main.nearClipPlane + 1;

        return Camera.main.ScreenToWorldPoint(mousePositionScreenSpace);
    }

    private bool IsValidPosition()
    {
        return true;
    }
}
