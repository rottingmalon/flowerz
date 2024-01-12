using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtSfx;
    [SerializeField] private GameObject fusionObject;

    private AudioSource _dirtSource;
    private AudioSource _fusionSource;

    private void Start()
    {
        _dirtSource = dirtSfx.GetComponent<AudioSource>();
    }

    public void PlayDirtSfx()
    {
        _dirtSource.pitch = Random.Range(1f, 1.5f);
        _dirtSource.Play();
    }

    public void PlayFusionSfx(Vector3 pos)
    {
        Instantiate(fusionObject, pos, Quaternion.identity);
        _fusionSource = fusionObject.GetComponent<AudioSource>();
        _fusionSource.pitch = Random.Range(1f, 1.5f);
        _fusionSource.Play();
    }
}
