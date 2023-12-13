using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinShop : MonoBehaviour
{

    [SerializeField] private Button skinSelectButton;
    [SerializeField] private Image imageButton;
    [SerializeField] private GameObject selectImage;
    

    //[SerializeField] private CanvasSkinShop canvasSkinShop;
    private ItemData itemData;
    

    private void OnEnable()
    {

    }

    public void SetData(ItemData itemData,ButtonSkinShop buttonSkinShop,Action<ItemData, ButtonSkinShop> callBack)
    {
        this.itemData = itemData;
        imageButton.sprite = itemData.itemSprite;
        // click
        skinSelectButton.onClick.AddListener(() => {
            callBack?.Invoke(itemData, buttonSkinShop);
            
        });
    }

    public void TurnOnImageChooseSkin()
    {
        selectImage.SetActive(true);
    }

    public void TurnOffImageChooseSkin()
    {
        selectImage.SetActive(false);
    }
}
