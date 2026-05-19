using UnityEngine;

public class PlacementState : PlayerControlState
{
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

        this.UpdateAllPegHighlights();

        //Continuously update equipped object state, and check to see if it can be placed in a location

        //If LMB and valid position, then place the object
        if (Input.GetMouseButtonDown(0) == true && this.IsValidPosition())
        {
            LevelObject objectInstance = PlayerControlsManager.instance.PlaceEquippedObject(this.GetMouseWorldPosition());                       
        }
    }

    private void UpdateAllPegHighlights()
    {
        PlayerControlsManager.instance.equippedLevelObjectInstance.UpdateHighlights();
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

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePositionScreenSpace);

        return new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0.0f);
    }

    private bool IsValidPosition()
    {
        return PlayerControlsManager.instance.equippedLevelObjectInstance.IsValidPosition();
    }
}
