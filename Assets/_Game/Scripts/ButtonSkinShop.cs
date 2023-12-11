using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinShop : MonoBehaviour
{
    
    [SerializeField] private Button skinSelectButton;
    [SerializeField] private CanvasSkinShop canvasSkinShop;

    private void Start()
    {
        skinSelectButton.onClick.AddListener(ChangeCurrentButton);
    }

    private void ChangeCurrentButton()
    {
         
        
    }
}
