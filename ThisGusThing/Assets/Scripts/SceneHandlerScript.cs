using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandlerScript : MonoBehaviour {

    public void ChangeSceneToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeSceneToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ChangeSceneToCredits()
    {
        SceneManager.LoadScene("EndCredits");
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
