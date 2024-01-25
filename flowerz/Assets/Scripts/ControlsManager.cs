using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    [Header("flowerz")]
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject yellowFlower;
    
    [Header("UI")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsMenuUI;
    [Space]
    [SerializeField] private GameObject redUI;
    [SerializeField] private GameObject blueUI;
    [SerializeField] private GameObject yellowUI;
    [SerializeField] private float scrollSpeed = 10;

    private static bool _isGamePaused;

    private GameObject _selectedFlower = null;
    private Camera _cam;
    
    private GameObject _audioManagerObject;
    private AudioManager _audioManager;

    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Game")
        {
            _isGamePaused = false;
        }
        else
        {
            _isGamePaused = true;
        }

        _selectedFlower = redFlower;
        _cam = Camera.main;
        
        _audioManagerObject = GameObject.FindGameObjectWithTag("AudioManager");
        _audioManager = _audioManagerObject.GetComponent<AudioManager>();
    }
    private void Update()
    {
        #region SWITCH FLOWER
        if (Input.GetKeyUp("1"))
        {
            SelectRedFlower();
        }
        else if (Input.GetKeyUp("2"))
        {
            SelectBlueFlower();
        }
        else if (Input.GetKeyUp("3"))
        {
            SelectYellowFlower();
        }
        #endregion
    
        //zoom
        _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * scrollSpeed, 30f, 45f);

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
        _audioManager.PlayDirtSfx();
        Instantiate(_selectedFlower, hit.point, Quaternion.identity);
    }
    
    #region SELECTION

    public void SelectRedFlower()
    {
        _selectedFlower = redFlower;
        redUI.SetActive(true);
        blueUI.SetActive(false);
        yellowUI.SetActive(false);
    }

    public void SelectBlueFlower()
    {
        _selectedFlower = blueFlower;
        redUI.SetActive(false);
        blueUI.SetActive(true);
        yellowUI.SetActive(false);
    }

    public void SelectYellowFlower()
    {
        _selectedFlower = yellowFlower;
        redUI.SetActive(false);
        blueUI.SetActive(false);
        yellowUI.SetActive(true);
    }

    #endregion

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
    public void ShowOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void HideOptions()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void BiggerButton(GameObject button)
    {
        button.transform.localScale *= new Vector2(1.25f, 1.25f);
    }
    public void SmallerButton(GameObject button)
    {
        button.transform.localScale = new Vector2(1f, 1f);
    }
    #endregion
}
