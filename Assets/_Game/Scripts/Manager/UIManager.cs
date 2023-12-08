using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject canvaJoystick;
    [SerializeField] private GameObject finishUI;
    [SerializeField] private GameObject weaponShopUI;
    private void Start()
    {
        OpenMainMenu();
    }
    public void OpenMainMenu()
    {
        mainMenuUI.SetActive(true);
        canvaJoystick.SetActive(false);
        weaponShopUI.SetActive(false);
        finishUI.SetActive(false);
    }

    public void DoPlayButton()
    {
        GameManager.Instance.ChangeStage(GameState.GamePlay);
        mainMenuUI.SetActive(false);
        canvaJoystick.SetActive(true);
    }

    public void OpenWeaponShop()
    {
        weaponShopUI.SetActive(true);
        mainMenuUI.SetActive(false);

    }

    public void CloseWeaponShop()
    {
        weaponShopUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    
    public void OpenFinishUI()
    {
        finishUI.SetActive(true);
    }

    public void CloseFinishUI()
    {
        GameManager.Instance.ChangeStage(GameState.MainMenu);
        finishUI.SetActive(false);
        mainMenuUI.SetActive(true);
        canvaJoystick.SetActive(false);
    }
}
