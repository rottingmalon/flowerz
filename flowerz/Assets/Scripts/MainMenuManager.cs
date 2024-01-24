using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
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

    public void ScaleUpButton(GameObject button)
    {
        button.transform.localScale *= new Vector2(1.25f, 1.25f);
    }
    
    public void ScaleDownButton(GameObject button)
    {
        button.transform.localScale = new Vector2(0.7879f, 0.7879f);
    }

    public void Quit()
     {
         Application.Quit();
     }
}
