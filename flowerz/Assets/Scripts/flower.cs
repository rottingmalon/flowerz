using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Flower : MonoBehaviour
{
    #region VAR
    [SerializeField] private float growAmount;
    [SerializeField] private float fuseRadius;
    public string attribute;
    private GameObject _flowerManagerObject;
    private FlowerManager _flowerManager;

    
    //private string[] fusions;
    //private float produceAmount;
    //private float produceTime;
    #endregion

    #region FUN
    private void Start()
    {
        _flowerManagerObject = GameObject.FindGameObjectWithTag("FlowerManager");
        _flowerManager = _flowerManagerObject.GetComponent<FlowerManager>();
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

        //overlap sphere to make list of all fuse radii in range
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

        //sort list by distance
        overlappedFlowers = overlappedFlowers.OrderBy(
            x => Vector2.Distance(this.transform.position,x.transform.position)
        ).ToList();

        //fuse with nearest fully grown
        foreach (var flower in overlappedFlowers)
        {
            if (flower.GetComponent<Flower>().growAmount >= 100)
            {
                _flowerManager.Fuse(gameObject, flower);
                return;
            }
        }
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, fuseRadius);
    }
}
