using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;

public class Flower : MonoBehaviour
{
    #region VAR
    [SerializeField] private float growAmount;
    [SerializeField] private float fuseRadius;
    [SerializeField] private List<string> fusions;
    [SerializeField] private Ease easeMode;
    
    public string attribute;
    private GameObject _flowerManagerObject;
    private FlowerManager _flowerManager;
    
    //private float produceAmount;
    //private float produceTime;
    #endregion

    #region FUN
    private void Start()
    {
        DOTween.Init();
        transform.DOScaleY(0, 0f);
        _flowerManagerObject = GameObject.FindGameObjectWithTag("FlowerManager");
        _flowerManager = _flowerManagerObject.GetComponent<FlowerManager>();
        Grow();
    }

    private void Update()
    {
            transform.DOScaleY(growAmount, 0f);
    }

    private void Grow()
    {
        DOTween.To(() => growAmount, x => growAmount = x, 1f, 1f).SetEase(easeMode).OnComplete(Launch()).SetAutoKill(true);
    }

    private TweenCallback Launch()
    {
        Invoke(nameof(CheckFusions), 1.5f);
        return null;
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

        //sort list by distance
        overlappedFlowers = overlappedFlowers.OrderBy(
            x => Vector2.Distance(this.transform.position,x.transform.position)
        ).ToList();

        //fuse with nearest possible & fully grown
        foreach (var flower in overlappedFlowers)
        {
            if (flower.GetComponent<Flower>().growAmount >= 1)
            {
                foreach (var fusionAttribute in fusions)
                {
                    if (flower.GetComponent<Flower>().attribute == fusionAttribute)
                    {
                        _flowerManager.Fuse(gameObject, flower);
                        return;
                    }
                }
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
