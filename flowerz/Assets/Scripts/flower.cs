using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class flower : MonoBehaviour
{
    #region VAR
    public float growAmount;
    [HideInInspector] public float fuseRadius;
    private string[] fusions;
    private string attribute;

    //private float produceAmount;
    //private float produceTime;

    #endregion

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

    public void fuse() 
    {
        //observer qui verif a l'instanciation
    }
    #endregion
}
