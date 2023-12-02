using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot_Isometric : MonoBehaviour
{
    private float targetAngle = 0f;
    private float currentAngle = 0.0f;
    private float mouseSensivity = 2f;
    private float rotationSpeed = 5f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1)) 
        {
            targetAngle += mouseX * mouseSensivity;
        }

        if (targetAngle < 0) 
        {
            targetAngle += 360;
        }

        if (targetAngle > 360) 
        {
            targetAngle -= 360;
        }

        currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(30, currentAngle, 0);
    }
}
