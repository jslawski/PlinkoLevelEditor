using UnityEngine;

public class PlacementState : PlayerControlState
{
    private GameObject _placeableObject;

    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        if (PlayerControlsManager.instance.equippedLevelObjectInstance != null)
        {
            this.UpdateObjectPosition();    
        }
    
    //Continuously update equipped object state, and check to see if it can be placed in a location
    
        //If LMB and valid position, then place the object



    }

    public override void ExitState()
    {

    }

    private void UpdateObjectPosition()
    {
        Vector3 mousePositionScreenSpace = Input.mousePosition;
        mousePositionScreenSpace.z = Camera.main.nearClipPlane + 1;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePositionScreenSpace);

        PlayerControlsManager.instance.equippedLevelObjectInstance.transform.position = mouseWorldPosition;
    }
}
