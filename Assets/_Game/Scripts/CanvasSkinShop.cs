using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : MonoBehaviour
{
    [SerializeField] private GameObject spawnShopCanvasPos;
    [SerializeField] private GameObject currtentCanvaShop;
    [Space]

    [SerializeField] private Button hatShopButton;
    [SerializeField] private GameObject hatShopPrefab;
    [Space]

    [SerializeField] private Button pantsShopButton;
    [SerializeField] private GameObject pantsShopPrefab;
    [Space]

    [SerializeField] private Button sheildShopButton;
    [SerializeField] private GameObject sheildShopPrefab;
    [Space]

    [SerializeField] private Button fullSetShopButton;
    [SerializeField] private GameObject fullSetShopPrefab;
    [Space]

    [SerializeField] private Button buyButton;
    [SerializeField] private Button unlockOneTimeButton;

    private Button currentButton;


    private void Start()
    {
        hatShopButton.onClick.AddListener(OpenHatShop);
        pantsShopButton.onClick.AddListener(OpenPantShop);
        sheildShopButton.onClick.AddListener(OpenSheildShop);
        fullSetShopButton.onClick.AddListener(OpenFullSetShop);
        buyButton.onClick.AddListener(DoBuyButton);
        unlockOneTimeButton.onClick.AddListener(DoUnlockOneTimeButton);
        OpenHatShop();
    }
    private void OpenHatShop()
    {
        ChangeCanvaShop(hatShopPrefab);
    }

    private void OpenPantShop()
    {
        ChangeCanvaShop(pantsShopPrefab);
    }

    private void OpenSheildShop()
    {
        ChangeCanvaShop(sheildShopPrefab);
    }

    private void OpenFullSetShop()
    {
        ChangeCanvaShop(fullSetShopPrefab);
    }

    private void DoBuyButton()
    {
        //TO DO: Buy weapon if u have enough gold
    }

    private void DoUnlockOneTimeButton()
    {
        //TO DO: Pop up Ad and unlock one time item
    }

    private void ChangeCanvaShop(GameObject shopPrefabs)
    {
        if(currtentCanvaShop == null)
        {
            currtentCanvaShop = Instantiate(shopPrefabs,spawnShopCanvasPos.transform);
        }
        else
        {
            Destroy(currtentCanvaShop);
            currtentCanvaShop = Instantiate(shopPrefabs, spawnShopCanvasPos.transform);
        }
    }

}
