using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject greenFlower;
    [SerializeField] private float scrollSpeed = 20;

    private GameObject _selectedFlower = null;
    private Camera _cam;
    private float _fov;

    private void Start()
    {
        _selectedFlower = redFlower;
        _cam = Camera.main;
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

        if (!Input.GetMouseButtonDown(0)) return;
        var r = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(r, out hit)) return;
        if (!hit.collider.CompareTag("Ground")) return;
        Instantiate(_selectedFlower, hit.point, Quaternion.identity);
    }
}
