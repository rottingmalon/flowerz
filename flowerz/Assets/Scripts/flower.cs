using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class flower 
{
    #region VAR
    private float growAmount;
    private float fuseRadius;
    private string[] fusions;
    private string attribute;

    //private float produceAmount;
    //private float produceTime;

    #endregion

    public flower(string color, float radius) 
    {
        growAmount = 0f;
        attribute = color;
        fuseRadius = radius;
    }

    #region FUN
    /*private void produce() 
    {
    }*/

    private void grow() 
    {
        while (growAmount < 100) 
        {
            growAmount += 1;
        }
    }

    private void fuse() 
    {
    }
    #endregion
}
