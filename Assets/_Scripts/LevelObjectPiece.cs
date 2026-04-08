using UnityEngine;

[CreateAssetMenu(fileName = "LevelObjectPiece", menuName = "ScriptableObjects/LevelObjectPiece", order = 1)]
public class LevelObjectPiece : ScriptableObject
{
    public GameObject objectPrefab;

    public Sprite objectThumbnail;    
}