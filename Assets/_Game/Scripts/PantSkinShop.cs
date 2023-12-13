using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantSkinShop : MonoBehaviour
{
    [SerializeField] private ButtonSkinShop buttonSkinShopPrb;
    [SerializeField] private Transform spawnButtonPos;
    [SerializeField] private PantDataSO pantDataSO;
    private ButtonSkinShop currentButton;
    private void Start()
    {
        List<PantData> pants = pantDataSO.pantDataList;
        for (int i = 0; i < pants.Count; i++)
        {
            //buttonSkinShop.ChangeImageItem(pants[i].itemSprite, pants[i].skinItemType);
            ButtonSkinShop skinItem = Instantiate(buttonSkinShopPrb, spawnButtonPos);
            skinItem.SetData(pants[i],skinItem, OnItemDataHandle);
        }
    }

    private void OnItemDataHandle(ItemData itemData,ButtonSkinShop buttonSkinShop)
    {
        Debug.Log(itemData.skinItemType.ToString());
        Debug.Log(itemData.itemMaterial.ToString());
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
}
