using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject canvaJoystick;
    [SerializeField] private Button playerButton;
    [SerializeField] private Canvas testFinish;
    private void OnEnable()
    {
        playerButton.onClick.AddListener(DoPlayButton);
    }



    private void DoPlayButton()
    {
        mainMenu.SetActive(false);
        canvaJoystick.SetActive(true);
        GameManager.Instance.ChangeStage(GameState.GamePlay);
    }

    public void DoFinish()
    {
        testFinish.gameObject.SetActive(true);
    }

}
