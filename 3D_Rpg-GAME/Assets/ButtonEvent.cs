using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvent : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("game scenes");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
