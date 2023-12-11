using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantSkinShop : MonoBehaviour
{
    [SerializeField] private ButtonSkinShop buttonSkinShop;
    [SerializeField] private Transform spawnButtonPos;
    [SerializeField] private PantDataSO pantDataSO;

    private void Start()
    {
        List<PantData> pants = pantDataSO.pantDataList;
        for (int i = 0; i < pants.Count; i++)
        {
            //buttonSkinShop.ChangeImageItem(pants[i].itemSprite, pants[i].skinItemType);
            Instantiate(buttonSkinShop, spawnButtonPos);
        }
    }
}
