using System;
using System.Collections.Generic;
using UnityEngine;

public class HatSkinShop : MonoBehaviour
{
    [SerializeField] private ButtonSkinShop buttonSkinShopPrb;
    [SerializeField] private Transform spawnButtonPos;
    [SerializeField] private HatDataSO hatDataSO;

    private void Start()
    {
        List<HatData> hats = hatDataSO.hatDataList;
        for (int i = 0; i < hats.Count; i++)
        {
            //buttonSkinShop.ChangeImageItem(hats[i].itemSprite, hats[i].skinItemType);
            ButtonSkinShop skinItem = Instantiate(buttonSkinShopPrb, spawnButtonPos);
            skinItem.SetData(hats[i], OnItemDataHandle);
        }
    }

    private void OnItemDataHandle(ItemData itemData)
    {

        Debug.Log(itemData.skinItemType.ToString());
    }
}
