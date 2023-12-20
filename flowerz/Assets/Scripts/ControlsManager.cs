using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject greenFlower;
    private string[,] _attributesArray;

    private GameObject _selectedFlower = null;
    private Camera _cam;

    private void Start()
    {
        _selectedFlower = redFlower;
        _cam = Camera.main;
        _attributesArray = new string[4, 4]
        {
            {null, "red", "blue", "green"},
            {"red", null, "purple", "yellow"},
            {"blue", "purple", null, "cyan"},
            {"green", "yellow", "cyan", null}
        };
    }
    private void Update()
    {
        #region SWITCH FLOWER
        if (Input.GetKeyUp("1"))
        {
            _selectedFlower = redFlower;
        }
        else if (Input.GetKeyUp("2"))
        {
            _selectedFlower = blueFlower;
        }
        else if (Input.GetKeyUp("3"))
        {
            _selectedFlower = greenFlower;
        }
        #endregion

        if (!Input.GetMouseButtonDown(0)) return;
        var r = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(r, out hit) && CompareTag("Ground")) return;
        Instantiate(_selectedFlower, hit.point, Quaternion.identity);
    }
}
