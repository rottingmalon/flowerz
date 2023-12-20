using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    private string[,] _attributesArray;

    private void Start()
    {
        _attributesArray = new string[4, 4]
        {
            {null, "red", "blue", "green"},
            {"red", null, "magenta", "yellow"},
            {"blue", "magenta", null, "cyan"},
            {"green", "yellow", "cyan", null}
        };
    }

    public void Fuse(GameObject flower1, GameObject flower2)
    {
        Destroy(flower1);
        Destroy(flower2);
    }
}
