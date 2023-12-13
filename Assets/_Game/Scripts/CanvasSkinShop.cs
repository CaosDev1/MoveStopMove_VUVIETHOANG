using UnityEngine;
using UnityEngine.UI;

public class CanvasSkinShop : MonoBehaviour
{
    [SerializeField] private Transform spawnShopCanvasPos;
    private GameObject currtentCanvaShop;
    [Space]

    [SerializeField] private Button hatShopButton;
    [SerializeField] private GameObject hatShopPrefab;
    private HatSkinShop hatSkinShop;
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
    [Space]

    [SerializeField] private Button closeButton;     
    

    private void Start()
    {
        hatShopButton.onClick.AddListener(OpenHatShop);
        pantsShopButton.onClick.AddListener(OpenPantShop);
        sheildShopButton.onClick.AddListener(OpenSheildShop);
        fullSetShopButton.onClick.AddListener(OpenFullSetShop);
        buyButton.onClick.AddListener(DoBuyButton);
        unlockOneTimeButton.onClick.AddListener(DoUnlockOneTimeButton);

        closeButton.onClick.AddListener(CloseSkinShop);
        OpenHatShop();
        
    }
    private void OpenHatShop()
    {
        SwitchCanvaShop(hatShopPrefab);
        hatSkinShop = hatShopPrefab.GetComponent<HatSkinShop>();
    }

    private void OpenPantShop()
    {
        SwitchCanvaShop(pantsShopPrefab);
    }

    private void OpenSheildShop()
    {
        SwitchCanvaShop(sheildShopPrefab);
    }

    private void OpenFullSetShop()
    {
        SwitchCanvaShop(fullSetShopPrefab);
    }

    private void DoBuyButton()
    {
        //TO DO: Buy weapon if u have enough gold
        hatSkinShop.BuyHatSkin();
    }

    private void DoUnlockOneTimeButton()
    {
        //TO DO: Pop up Ad and unlock one time item
    }

    private void CloseSkinShop()
    {
        UIManager.Instance.CloseSkinShopUI();
    }

    private void SwitchCanvaShop(GameObject canvaShop)
    {
        if(currtentCanvaShop == null)
        {
            currtentCanvaShop = canvaShop;
            currtentCanvaShop.SetActive(true);
        }
        else
        {
            currtentCanvaShop.SetActive(false);
            currtentCanvaShop = canvaShop;
            currtentCanvaShop.SetActive(true);
        }
    }
    private void ChangeCanvaShop(GameObject shopPrefabs)
    {
        if (currtentCanvaShop == null)
        {
            currtentCanvaShop = Instantiate(shopPrefabs, spawnShopCanvasPos);
        }
        else if(currtentCanvaShop != shopPrefabs)
        {
            Destroy(currtentCanvaShop);
            currtentCanvaShop = Instantiate(shopPrefabs, spawnShopCanvasPos);
        }
    }

}
