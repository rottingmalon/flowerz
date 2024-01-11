using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtSfx;

    private AudioSource _dirtSource;

    private void Start()
    {
        _dirtSource = dirtSfx.GetComponent<AudioSource>();
    }

    public void PlayDirtSfx()
    {
        _dirtSource.pitch = Random.Range(1f, 1.5f);
        _dirtSource.Play();

        //_dirtSource.pitch = 1f;
    }
}
