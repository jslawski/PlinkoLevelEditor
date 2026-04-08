using System.Collections.Generic;
using UnityEngine;

public class LevelObjectButtonGridManager : MonoBehaviour
{
    public static LevelObjectButtonGridManager instance;

    private List<LevelObjectButton> _allLevelObjectButtons;

    private LevelObjectButton _currentlySelectedButton;

    [SerializeField]
    private GameObject _buttonPrefab;

    [SerializeField]
    private Transform _buttonSpawnGridParent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        this._allLevelObjectButtons = new List<LevelObjectButton>();

        this.LoadAllLevelObjectButtons();
    }

    private void LoadAllLevelObjectButtons()
    {
        LevelObjectPiece[] allLevelObjectPieces = Resources.LoadAll<LevelObjectPiece>("LevelObjectPieces");

        for (int i = 0; i < allLevelObjectPieces.Length; i++)
        {
            GameObject newButtonInstance = Instantiate(this._buttonPrefab, this._buttonSpawnGridParent);
            LevelObjectButton buttonComponent = newButtonInstance.GetComponent<LevelObjectButton>();
            buttonComponent.SetupButton(allLevelObjectPieces[i]);

            this._allLevelObjectButtons.Add(buttonComponent);
        }
    }

    public void UpdateButtonGrid(LevelObjectButton selectedButton)
    {
        for (int i = 0; i < this._allLevelObjectButtons.Count; i++)
        {
            if (selectedButton == this._allLevelObjectButtons[i])
            {
                this._allLevelObjectButtons[i].DisableButton();
            }
            else
            {
                this._allLevelObjectButtons[i].EnableButton();
            }
        }
    }
}
