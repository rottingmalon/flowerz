using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class flower : MonoBehaviour
{
    #region VAR
    public float growAmount;
    public float fuseRadius;
    
    //private string[] fusions;
    //private string attribute;
    //private float produceAmount;
    //private float produceTime;
    #endregion

    #region FUN
    private void Start()
    {
        Grow();
    }

    private void Grow()
    {
        while (growAmount < 100) 
        {
            //Debug.Log(growAmount);
            growAmount += 1;
        }
        CheckFusions();
    }
    
    private void CheckFusions() 
    {
        //create flower list
        List<GameObject> overlappedFlowers = new List<GameObject>();

        //overlapsphere to make list of all fuseRadiuses in range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, fuseRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("FlowerRadius") && hitCollider.transform.parent.gameObject != gameObject)
            {
                overlappedFlowers.Add(hitCollider.transform.parent.gameObject);
            }
        }    

        foreach (var flower in overlappedFlowers)
        {
            Debug.Log(flower);
        }

        //compute distances between parents

        //sort list by distance

        //fuse with nearest fully grown
    }

    public void fuse() 
    {
        //observer qui verif a l'instanciation
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, fuseRadius);
    }
}
