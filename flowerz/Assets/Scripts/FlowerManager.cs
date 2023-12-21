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
    [SerializeField] private GameObject pinkFlower;
    [SerializeField] private GameObject purpleFlower;
    [SerializeField] private GameObject skyblueFlower;
    [SerializeField] private GameObject turquoiseFlower;
    [SerializeField] private GameObject applegreenFlower;
    [SerializeField] private GameObject orangeFlower;
    [SerializeField] private GameObject whiteFlower;
    #endregion
    
    private string[,] _attributesArray;
    
    #endregion
    private void Start()
    {
        _attributesArray = new string[7, 7]
        {
            {null, "red", "blue", "green", "magenta", "yellow", "cyan"},
            {"red", null, "magenta", "yellow", "pink", "orange", "white"},
            {"blue", "magenta", null, "cyan", "purple", "white", "skyblue"},
            {"green", "yellow", "cyan", null, "white", "applegreen", "turquoise"},
            {"magenta", "pink", "purple", "white", null, null, null},
            {"yellow", "orange", "white", "applegreen", null, null, null},
            {"cyan", "white", "skyblue", "turquoise", null, null, null},
        };
    }

    public void Fuse(GameObject flower1, GameObject flower2)
    {
        var floPos = flower1.transform.position;

        var newAttribute = SelectAttribute(flower1, flower2);
        
        if (newAttribute == null) return;
        Debug.Log(flower1.name + "," + flower2.name + "=" + newAttribute );
        
        var tempFlower = SelectFlower(newAttribute);
        
        var tempDir = (flower2.transform.position - floPos) / 2;
        var tempPos = floPos + tempDir;

        if (tempFlower == null) return;
        Destroy(flower1);
        Destroy(flower2);
        Instantiate(tempFlower, tempPos, Quaternion.identity);
    }

    private string SelectAttribute(GameObject flower1, GameObject flower2)
    {
        var x = 0;
        var y = 0;
        
        for (var i = 0; i < 7; i++)
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
            case ("pink"):
                fusion = pinkFlower;
                break;
            case ("purple"):
                fusion = purpleFlower;
                break;
            case ("skyblue"):
                fusion = skyblueFlower;
                break;
            case ("turquoise"):
                fusion = turquoiseFlower;
                break;
            case ("applegreen"):
                fusion = applegreenFlower;
                break;
            case ("orange"):
                fusion = orangeFlower;
                break;
            case ("white"):
                fusion = whiteFlower;
                break;
            default:
                break;
        }
        return (fusion);
    }
}
