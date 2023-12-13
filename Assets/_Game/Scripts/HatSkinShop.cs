using System.Collections.Generic;
using UnityEngine;

public class HatSkinShop : MonoBehaviour
{
    [SerializeField] private ButtonSkinShop buttonSkinShopPrb;
    [SerializeField] private Transform spawnButtonPos;
    [SerializeField] private HatDataSO hatDataSO;
    [SerializeField] private Player player;
    private ButtonSkinShop currentButton;
    private void Start()
    {
        List<HatData> hats = hatDataSO.hatDataList;
        for (int i = 0; i < hats.Count; i++)
        {
            //buttonSkinShop.ChangeImageItem(hats[i].itemSprite, hats[i].skinItemType);
            ButtonSkinShop skinItem = Instantiate(buttonSkinShopPrb, spawnButtonPos);
            skinItem.SetData(hats[i], skinItem, OnItemDataHandle);
        }
    }

    private void OnItemDataHandle(ItemData hatItemData,ButtonSkinShop buttonSkinShop)
    {
        player.PlayerHatType = hatItemData.skinItemType;

        ChangeImageChooseSkin(buttonSkinShop);
    }

    private void ChangeImageChooseSkin(ButtonSkinShop buttonSkinShop)
    {
        if (currentButton == null)
        {
            currentButton = buttonSkinShop;
            currentButton.TurnOnImageChooseSkin();
        }
        else if (currentButton != buttonSkinShop)
        {
            currentButton.TurnOffImageChooseSkin();
            currentButton = buttonSkinShop;
            currentButton.TurnOnImageChooseSkin();

        }
    }

    public void BuyHatSkin()
    {
        player.HatData = DataManager.Instance.GetHatData(player.PlayerHatType);
        player.SpawnHatSkin(player.HatData.itemPrefab);
        DataManager.Instance.ChangeHatSkin(player.PlayerHatType);
    }
}
