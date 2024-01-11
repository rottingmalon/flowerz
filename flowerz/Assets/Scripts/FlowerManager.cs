using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FlowerManager : MonoBehaviour
{
    #region VAR

    #region FLOWERZ

    [Header("Basic")] [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject yellowFlower;

    [Header("Advanced")] [SerializeField] private GameObject purpleFlower;
    [SerializeField] private GameObject greenFlower;
    [SerializeField] private GameObject orangeFlower;

    [Header("Advanced +")] [SerializeField]
    private GameObject turquoiseFlower;

    [SerializeField] private GameObject lightOrangeFlower;
    [SerializeField] private GameObject darkOrangeFlower;
    [SerializeField] private GameObject applegreenFlower;
    [SerializeField] private GameObject crimsonFlower;
    [SerializeField] private GameObject darkBlueFlower;

    [Header("Special")] [SerializeField] private GameObject whiteFlower;
    [SerializeField] private GameObject blackFlower;

    #endregion
    
    
    [FormerlySerializedAs("fusionPSR")]
    [Header("PS")]
    #region PS
    [SerializeField] private GameObject fusionPsr;
    [SerializeField] private GameObject fusionPsb;
    [SerializeField] private GameObject fusionPsy;
    [SerializeField] private GameObject fusionPsg;
    [SerializeField] private GameObject fusionPsp;
    [SerializeField] private GameObject fusionPso;
    [SerializeField] private GameObject fusionPsw;
    #endregion
    
    [Space]
    private GameObject _fusionPS;
    private GameObject _fusionPS1;

    private string[,] _attributesArray;

    #endregion

    private void Start()
    {
        #region ARRAY

        _attributesArray = new string[8, 8]
        {
            { null, "red", "blue", "yellow", "purple", "green", "orange", "white" },
            { "red", null, "purple", "orange", "crimson", "white", "darkorange", null },
            { "blue", "purple", null, "green", "darkblue", "turquoise", "white", null },
            { "yellow", "orange", "green", null, "white", "applegreen", "lightorange", null },
            { "purple", "crimson", "darkblue", "white", null, null, null, null },
            { "green", "white", "turquoise", "applegreen", null, null, null, null },
            { "orange", "darkorange", "white", "darkorange", null, null, null, null },
            { "white", null, null, null, null, null, null, "black" },
        };

        #endregion
    }

    public void Fuse(GameObject flower1, GameObject flower2)
    {
        var floPos = flower1.transform.position;

        var newAttribute = SelectAttribute(flower1, flower2);

        if (newAttribute == null) return;
        //print(flower1.name + "," + flower2.name + "=" + newAttribute );

        var tempFlower = SelectFlower(newAttribute);

        var tempDir = (flower2.transform.position - floPos) / 2;
        var tempPos = floPos + tempDir;

        if (tempFlower == null) return;

        //CreateFusionPS(flower1, flower2, tempPos);
        _fusionPS = SelectFusionPS(flower1.GetComponent<Flower>().attribute);
        _fusionPS1 = SelectFusionPS(flower2.GetComponent<Flower>().attribute);
        Instantiate(_fusionPS, tempPos, Quaternion.Euler(-90, 0, 0));
        Instantiate(_fusionPS1, tempPos, Quaternion.Euler(-90, 0, 0));

        //make an instantiate coroutine
        StartCoroutine(Fusion(flower1, flower2, tempFlower, tempPos));
    }
    
    private string SelectAttribute(GameObject flower1, GameObject flower2)
    {
        var x = 0;
        var y = 0;

        for (var i = 0; i < 8; i++)
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

        return (_attributesArray[x, y]);
    }

    private GameObject SelectFlower(string attribute)
    {
        GameObject fusion = null;

        switch (attribute)
        {
            case ("purple"):
                fusion = purpleFlower;
                break;
            case ("green"):
                fusion = greenFlower;
                break;
            case ("orange"):
                fusion = orangeFlower;
                break;
            case ("turquoise"):
                fusion = turquoiseFlower;
                break;
            case ("applegreen"):
                fusion = applegreenFlower;
                break;
            case ("lightorange"):
                fusion = lightOrangeFlower;
                break;
            case ("darkorange"):
                fusion = darkOrangeFlower;
                break;
            case ("crimson"):
                fusion = crimsonFlower;
                break;
            case ("darkblue"):
                fusion = darkBlueFlower;
                break;
            case ("white"):
                fusion = whiteFlower;
                break;
            case ("black"):
                fusion = blackFlower;
                break;
            default:
                break;
        }

        return (fusion);
    }

    private GameObject SelectFusionPS(string attribute)
    {
        return attribute switch
        {
            ("red") => (fusionPsr),
            ("blue") => (fusionPsb),
            ("yellow") => (fusionPsy),
            ("purple") => (fusionPsp),
            ("green") => (fusionPsg),
            ("orange") => (fusionPso),
            ("white") => (fusionPsw),
            _ => (null)
        };
    }

    private IEnumerator Fusion(GameObject flower1, GameObject flower2, GameObject tempFlower, Vector3 tempPos)
    {
        flower1.GetComponent<Flower>().isDead = true;
        flower2.GetComponent<Flower>().isDead = true;
        
        yield return new WaitForSeconds(4f);
        Destroy(flower1);
        Destroy(flower2);
        Instantiate(tempFlower, tempPos, Quaternion.identity);
    }

    /*
    private Color PickPSColor(string attribute)
    {
        switch (attribute)
        {
            case ("red"):
                var colorR = new Color(217, 74, 74, 255);
                return (colorR);
            case ("blue"):
                var colorB = new Color(36, 138, 216, 255);
                return (colorB);
            case ("yellow"):
                var colorY = new Color(233, 241, 40, 255);
                return (colorY);
            case ("purple"):
                var colorP = new Color(152, 78, 244, 255);
                return (colorP);
            case ("green"):
                var colorG = new Color(152, 78, 244, 255);
                return (colorG);
            case ("orange"):
                var colorO = new Color(152, 78, 244, 255);
                return (colorO);
            case ("white"):
                var colorW = new Color(152, 78, 244, 255);
                return (colorW);
            default:
                return (new Color(0, 0, 0, 255));
        }
    }

    private void CreateFusionPS(GameObject flower1, GameObject flower2, Vector3 position)
    {
        var attribute1 = flower1.GetComponent<Flower>().attribute;
        var attribute2 = flower2.GetComponent<Flower>().attribute;

        _psColor.startColor = PickPSColor(attribute1);
        _psColor1.startColor = PickPSColor(attribute2);

        Instantiate(fusionPS, position, Quaternion.Euler(-90, 0, 0));
        Instantiate(fusionPS1, position, Quaternion.Euler(-90, 0, 0));
    }
    */
}
