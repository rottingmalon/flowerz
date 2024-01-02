using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject yellowFlower;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject redUI;
    [SerializeField] private GameObject blueUI;
    [SerializeField] private GameObject yellowUI;
    [SerializeField] private float scrollSpeed = 10;

    private static bool _isGamePaused;

    private GameObject _selectedFlower = null;
    private Camera _cam;

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "Game")
        {
            _isGamePaused = false;
        }
        else
        {
            _isGamePaused = true;
        }

        _selectedFlower = redFlower;
        _cam = Camera.main;
    }
    private void Update()
    {
        #region SWITCH FLOWER
        if (Input.GetKeyUp("1"))
        {
            _selectedFlower = redFlower;
            redUI.SetActive(true);
            blueUI.SetActive(false);
            yellowUI.SetActive(false);
        }
        else if (Input.GetKeyUp("2"))
        {
            _selectedFlower = blueFlower;
            redUI.SetActive(false);
            blueUI.SetActive(true);
            yellowUI.SetActive(false);
        }
        else if (Input.GetKeyUp("3"))
        {
            _selectedFlower = yellowFlower;
            redUI.SetActive(false);
            blueUI.SetActive(false);
            yellowUI.SetActive(true);
        }
        #endregion
    
        #region CAM
        if (_cam.fieldOfView <= 45f && _cam.fieldOfView >= 30f)
        {
            _cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        }
        else if (_cam.fieldOfView >= 45f)
        {
            _cam.fieldOfView = 45f;
        }
        else if (_cam.fieldOfView <= 30f)
        {
            _cam.fieldOfView = 30f;
        }
        #endregion

        #region PAUSE
        if (Input.GetKeyUp("escape"))
        {
            if (_isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        #endregion

        if (Input.GetKeyUp("f"))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
        }

        if (!Input.GetMouseButtonDown(0)) return;
        var r = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(r, out hit)) return;
        if (!hit.collider.CompareTag("Ground")) return;
        if (_isGamePaused) return;
        Instantiate(_selectedFlower, hit.point, Quaternion.identity);
    }

    #region MENUBUTTONS

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isGamePaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isGamePaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion
}
