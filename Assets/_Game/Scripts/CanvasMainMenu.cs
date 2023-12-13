using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button weaponShopButton;
    [SerializeField] private Button skinShopButton;
    [SerializeField] private Button zombieModeButton;
    [SerializeField] private Button removeADButton;
    [SerializeField] private Button removeBrrButton;
    [SerializeField] private Button removeSoundButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayeButtonOnClick);
        weaponShopButton.onClick.AddListener(WeaponShopButtonOnClick);
        skinShopButton.onClick.AddListener(SkinShopButtonOnClick);
    }

    private void PlayeButtonOnClick()
    {
        UIManager.Instance.DoPlayButton();
    }

    private void WeaponShopButtonOnClick()
    {
        UIManager.Instance.OpenWeaponShop();
    }

    private void SkinShopButtonOnClick()
    {
        UIManager.Instance.OpenSkinShopUI();
    }
}
