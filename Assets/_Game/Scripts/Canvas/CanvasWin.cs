using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWin : MonoBehaviour
{
    [SerializeField] private Button rewardButton;
    [SerializeField] private Button nextLevelButton;

    private void OnEnable()
    {
        rewardButton.onClick.AddListener(OpenAD);
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void OpenAD()
    {
        //Mo quang cao
        Debug.Log("Open AD");
    }

    private void NextLevel()
    {
        Debug.Log("next level");
        UIManager.Instance.CloseWinUI();
        UIManager.Instance.OpenMainMenu();
        LevelManager.Instance.NextLevel();
    }
}
