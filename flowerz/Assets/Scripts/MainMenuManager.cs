using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp("f"))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
        }
    }

    public void PlayGame()
     {
         Time.timeScale = 1f;
         SceneManager.LoadScene(1);
     }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
    /*public void Credits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }*/
 
     public void Quit()
     {
         Application.Quit();
     }
}
