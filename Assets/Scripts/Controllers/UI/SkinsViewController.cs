using System.Collections.Generic;
using UnityEngine;

public class SkinsViewController : MonoBehaviour
{
    [SerializeField] private Skins skins;
    [SerializeField] private GameObject skinItemPrefab;
    [SerializeField] private GameObject unlockButton;
    private readonly Dictionary<SkinData, SkinUIItemController> itemControllers = new();
    private SkinUIItemController selectedSkinUIItemController;

    private void Start()
    {
        InitializeBoard();

        if(skins.LockedSkinsCount == 0)
        {
            DisableUnlockButton();
        }
    }

    private void OnEnable()
    {
        SkinUIItemController.OnSkinSelected += OnSkinSelected;
    }

    private void OnDisable()
    {
        SkinUIItemController.OnSkinSelected -= OnSkinSelected;
    }

    private void InitializeBoard()
    {
        foreach (SkinData skinData in skins)
        {
            GameObject instance = Instantiate(skinItemPrefab, transform);
            SkinUIItemController skinItemController = instance.GetComponent<SkinUIItemController>();
            skinItemController.Initialize(skinData);
            itemControllers.Add(skinData, skinItemController);
        }

        SkinData selectedSkin = skins.selectedSkin;
        if (selectedSkin != null)
        {
            selectedSkinUIItemController = itemControllers[selectedSkin];
            selectedSkinUIItemController.Select();
        }
    }

    private void OnSkinSelected(SkinUIItemController controller)
    {
        if(selectedSkinUIItemController != null && selectedSkinUIItemController != controller) selectedSkinUIItemController.Unselect();
        selectedSkinUIItemController = controller;
        skins.selectedSkin = selectedSkinUIItemController.SkinData;
    }

    private void UnlockRandomItem()
    {
        SkinData skinData = skins.UnlockRandomSkin();
        if(skinData != null)
        {
            SkinUIItemController controller = itemControllers[skinData];

            if(controller != null)
            {
                controller.Unlock();
            }
        }
    }

    private void DisableUnlockButton()
    {
        unlockButton.SetActive(false);
    }

    public void UnlockButtonClickHandler()
    {
        if (skins.LockedSkinsCount > 0)
        {
            UnlockRandomItem();

            if (skins.LockedSkinsCount == 0)
            {
                DisableUnlockButton();
            }
        }
    }
}
