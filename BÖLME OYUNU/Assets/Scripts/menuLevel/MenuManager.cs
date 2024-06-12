using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]

    private GameObject startButton, exitButton;
    void Start()
    {

        FadeOut();
    }

    void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
