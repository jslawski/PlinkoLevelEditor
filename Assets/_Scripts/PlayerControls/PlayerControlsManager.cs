using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;

    public GameObject equippedLevelObjectPrefab;

    public GameObject equippedLevelObjectInstance;

    private PlayerControlState _currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._currentState = new PlacementState();
    }

    private void Update()
    {
        if (this._currentState != null)
        {
            this._currentState.UpdateState();
        }
    }

    public void EquipObject(GameObject objectPrefab)
    { 
        this.equippedLevelObjectPrefab = objectPrefab;

        Destroy(this.equippedLevelObjectInstance);

        this.equippedLevelObjectInstance = Instantiate(this.equippedLevelObjectPrefab, new Vector3(1000.0f, 1000.0f, 0.0f), new Quaternion());
        this.equippedLevelObjectInstance.GetComponent<LevelObject>().DisableCollision();
    }

    public void PlaceEquippedObject(Vector3 placementPosition)
    {
        Instantiate(this.equippedLevelObjectPrefab, placementPosition, new Quaternion());        
    }

    //Handle all of the clicking, dragging, and placing stuff in this file
}
