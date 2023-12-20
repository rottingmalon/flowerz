using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    #region VAR
    
    #region FLOWERZ
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject greenFlower;
    [SerializeField] private GameObject magentaFlower;
    [SerializeField] private GameObject yellowFlower;
    [SerializeField] private GameObject cyanFlower;
    #endregion
    
    private string[,] _attributesArray;
    
    #endregion
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
        var floPos = flower1.transform.position;

        var newAttribute = SelectAttribute(flower1, flower2);
        var tempFlower = SelectFlower(newAttribute);
        
        var tempDir = (flower2.transform.position - floPos) / 2;
        var tempPos = floPos + tempDir;

        if (tempFlower == null) return;
        Instantiate(tempFlower, tempPos, Quaternion.identity);
        Destroy(flower1);
        Destroy(flower2);
    }

    private string SelectAttribute(GameObject flower1, GameObject flower2)
    {
        var x = 0;
        var y = 0;
        
        for (var i = 0; i < 4; i++)
        {
            if (_attributesArray[i, 0] == flower1.GetComponent<Flower>().attribute)
            {
                x = i;
            }
            if (_attributesArray[0, i] == flower2.GetComponent<Flower>().attribute)
            {
                y = i;
            }
        }
        return(_attributesArray[x, y]);
    }

    private GameObject SelectFlower(string attribute)
    {
        GameObject fusion = null;

        switch (attribute)
        {
            case ("magenta"):
                fusion = magentaFlower;
                break;
            case ("yellow"):
                fusion = yellowFlower;
                break;
            case ("cyan"):
                fusion = cyanFlower;
                break;
            default:
                break;
        }
        return (fusion);
    }
}
