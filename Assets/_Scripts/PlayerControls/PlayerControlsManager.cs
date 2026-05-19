using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;

    [SerializeField]
    private GameObject _dynamicBoundingBoxPrefab;

    [HideInInspector]
    public GameObject equippedLevelObjectPrefab;
    [HideInInspector]
    public LevelObject equippedLevelObjectInstance;

    private PlayerControlState _currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
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

        GameObject levelObject = Instantiate(this.equippedLevelObjectPrefab, new Vector3(1000.0f, 1000.0f, 0.0f), new Quaternion());        
        this.equippedLevelObjectInstance = levelObject.GetComponent<LevelObject>();       
        this.equippedLevelObjectInstance.UpdateBounds();

        this._currentState = new PlacementState();
        this._currentState.EnterState();

        //GameObject dynamicBoundingBoxInstance = Instantiate(this._dynamicBoundingBoxPrefab, this.equippedLevelObjectInstance.transform);
        //dynamicBoundingBoxInstance.GetComponent<DynamicBoundingBox>().Setup(levelObjectComponent.minBounds, levelObjectComponent.maxBounds);

        this.equippedLevelObjectInstance.DisableCollision();        
    }

    public LevelObject PlaceEquippedObject(Vector3 placementPosition)
    {
        GameObject placedObject = Instantiate(this.equippedLevelObjectPrefab, placementPosition, new Quaternion());
        LevelObject levelObjectComponent = placedObject.GetComponent<LevelObject>();

        levelObjectComponent.UpdateBounds();
        levelObjectComponent.EnablePhysicsCollision();

        levelObjectComponent.DisableHighlights();

        return levelObjectComponent;
    }

    //Handle all of the clicking, dragging, and placing stuff in this file
}
