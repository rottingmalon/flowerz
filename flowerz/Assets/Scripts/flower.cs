using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class Flower : MonoBehaviour
{
    #region VAR
    [SerializeField] private GameObject pollen;
    [SerializeField] private GameObject burstPS;
    [SerializeField] private float fuseRadius;
    [SerializeField] private List<string> fusions;
    [SerializeField] private Ease easeMode;
    [SerializeField] private float growDuration;
    
    private float _growAmount;
    public string attribute;
    private GameObject _flowerManagerObject;
    private FlowerManager _flowerManager;

    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isFusing;
    private bool _canFuse;

    [SerializeField] private GameObject burstSfxObject;
    private AudioSource _burstSfxSource;
    
    //private float produceAmount;
    //private float produceTime;
    #endregion

    private void Start()
    {
        _canFuse = false;
        isFusing = false;
        isDead = false;
        _growAmount = 0f;
        
        DOTween.Init();
        transform.DOScaleY(0, 0f);
        
        _flowerManagerObject = GameObject.FindGameObjectWithTag("FlowerManager");
        _flowerManager = _flowerManagerObject.GetComponent<FlowerManager>();
        
        Grow();
        Invoke(nameof(PlayBurstSfx), growDuration);
        Invoke(nameof(ActivatePS), growDuration);
        Invoke(nameof(CheckFusions), growDuration + 1f);
    }

    private void Update()
    {
            transform.DOScaleY(_growAmount, 0f);

            if (isDead)
            {
                DOTween.To(() => _growAmount, x => _growAmount = x, 0f, 4f);
            }
    }

    private void Grow()
    {
        DOTween.To(() => _growAmount, x => _growAmount = x, 1f, growDuration).SetEase(easeMode);
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
            if (flower.GetComponent<Flower>()._canFuse)
            {
                foreach (var fusionAttribute in fusions)
                {
                    if (!this.isFusing)
                    {
                        if (flower.GetComponent<Flower>().attribute == fusionAttribute && !flower.GetComponent<Flower>().isFusing)
                        {
                            _flowerManager.Fuse(gameObject, flower);
                            isFusing = true;
                            return;
                        } 
                    }
                }
            }
        }
    }

    private void PlayBurstSfx()
    {
        Instantiate(burstSfxObject, this.transform.position, Quaternion.identity);
        _burstSfxSource = burstSfxObject.GetComponent<AudioSource>();
        _burstSfxSource.pitch = Random.Range(1f, 1.5f);
        _burstSfxSource.Play();
    }
    
    private void ActivatePS()
    {
        pollen.SetActive(true);
        burstPS.SetActive(true);
        _canFuse = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, fuseRadius);
    }
}
