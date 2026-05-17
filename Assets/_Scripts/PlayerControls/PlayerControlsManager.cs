using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;

    [SerializeField]
    private GameObject _dynamicBoundingBoxPrefab;

    [HideInInspector]
    public GameObject equippedLevelObjectPrefab;
    [HideInInspector]
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
        LevelObject levelObjectComponent = this.equippedLevelObjectInstance.GetComponent<LevelObject>();       
        levelObjectComponent.UpdateBounds();

        GameObject dynamicBoundingBoxInstance = Instantiate(this._dynamicBoundingBoxPrefab, this.equippedLevelObjectInstance.transform);
        dynamicBoundingBoxInstance.GetComponent<DynamicBoundingBox>().Setup(levelObjectComponent.minBounds, levelObjectComponent.maxBounds);

        levelObjectComponent.DisableCollision();        
    }

    public void PlaceEquippedObject(Vector3 placementPosition)
    {
        GameObject placedObject = Instantiate(this.equippedLevelObjectPrefab, placementPosition, new Quaternion());
        placedObject.GetComponent<LevelObject>().UpdateBounds();
    }

    //Handle all of the clicking, dragging, and placing stuff in this file
}
