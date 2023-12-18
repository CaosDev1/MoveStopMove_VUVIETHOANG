using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;

public class CanvasSkinShop : MonoBehaviour
{
    [SerializeField] private Transform spawnShopCanvasPos;
    private GameObject currtentCanvaShop;
    [SerializeField] private SkinShopType currentShopType;
    [Space]

    [SerializeField] private Button hatShopButton;
    [SerializeField] private GameObject hatShopPrefab;
    private HatSkinShop hatSkinShop;
    [Space]

    [SerializeField] private Button pantsShopButton;
    [SerializeField] private GameObject pantsShopPrefab;
    private PantSkinShop pantSkinShop;
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
        
        currentShopType = SkinShopType.HATSHOP;
    }

    private void OpenPantShop()
    {
        SwitchCanvaShop(pantsShopPrefab);
        pantSkinShop = pantsShopPrefab.GetComponent<PantSkinShop>();
        currentShopType = SkinShopType.PANTSHOP;
    }

    private void OpenSheildShop()
    {
        SwitchCanvaShop(sheildShopPrefab);
        currentShopType = SkinShopType.SHEILDSHOP;
    }

    private void OpenFullSetShop()
    {
        SwitchCanvaShop(fullSetShopPrefab);
        currentShopType = SkinShopType.FULLSETSHOP;
    }

    private void DoBuyButton()
    {
        switch (currentShopType)
        {
            case SkinShopType.HATSHOP:
                hatSkinShop.BuyHatSkin();
                break;
            case SkinShopType.PANTSHOP:
                //TO DO: Buy Pant
                pantSkinShop.BuyPantSkin();
                break;
            case SkinShopType.SHEILDSHOP:

                break;
            case SkinShopType.FULLSETSHOP:

                break;
            default:
                break;
        }
        
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

    public enum SkinShopType
    {
        HATSHOP = 0,
        PANTSHOP = 1,
        SHEILDSHOP = 2,
        FULLSETSHOP = 3,
    }
}
