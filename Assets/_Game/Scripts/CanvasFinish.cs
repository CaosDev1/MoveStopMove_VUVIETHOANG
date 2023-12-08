using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFinish : MonoBehaviour
{
    [SerializeField] private Button contiuneButton;
    [SerializeField] private Button rewardButton;

    private void Start()
    {
        contiuneButton.onClick.AddListener(DoContinue);
        rewardButton.onClick.AddListener(DoReward);
    }

    private void DoReward()
    {
        //TO DO: Pop up ad and reward
        Debug.Log("Open AD");
    }

    private void DoContinue()
    {
        LevelManager.Instance.ResetGame();
        UIManager.Instance.CloseFinishUI();
    }
}
