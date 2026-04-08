using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public static PlayerControlsManager instance;

    private GameObject _equippedLevelObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void EquipObject(GameObject objectPrefab)
    { 
        this._equippedLevelObject = objectPrefab;
    }

    //Handle all of the clicking, dragging, and placing stuff in this file
}
