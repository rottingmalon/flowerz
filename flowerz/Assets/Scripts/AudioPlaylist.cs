using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class AudioPlaylist : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioMixer audioMixer;
    private AudioSource _audioSource;
    
    [Header("Playlist UI")]
    [SerializeField] private TMP_Text songName;
    [SerializeField] private TMP_Text albumName;
    [SerializeField] private string[] albums;
    [SerializeField] private Image albumCover;
    [SerializeField] private Sprite[] covers;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool _fadeIn = false;
    private bool _fadeOut = false;
    private int _lastSongID;
    private bool _hasPlayedFirstClip;
    
    // Start is called before the first frame update
    private void Start()
    {
        //Fade
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "SoundFade", 20f, 1f));
        
        _hasPlayedFirstClip = false;
       _audioSource = gameObject.GetComponent<AudioSource>(); 
    }

    private AudioClip RandomClip()
    {
        while (true)
        {
            if (!_hasPlayedFirstClip)
            {
                var randomNumber = Random.Range(0, audioClips.Length);
                var randomClip = audioClips[randomNumber];
                _lastSongID = randomNumber;
                _hasPlayedFirstClip = true;
                return (randomClip);
            }
            else
            {
                var randomNumber = Random.Range(0, audioClips.Length);
                if (randomNumber != _lastSongID)
                {
                    var randomClip = audioClips[randomNumber];
                    _lastSongID = randomNumber;
                    return (randomClip);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = RandomClip();
            SetAlbum(_lastSongID);
            _audioSource.Play();
            _fadeIn = true;
            StartCoroutine(WaitForFade());
            canvasGroup.alpha -= Time.deltaTime;

            print(_audioSource.clip.name);
            songName.SetText(_audioSource.clip.name);
        }
        if (Input.GetKeyUp("9"))
        {
            _audioSource.Stop();
        }

        if (_fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                if (canvasGroup.alpha >= 1)
                {
                    _fadeIn = false;
                }
            }
        }
        else if (_fadeOut)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
            }
            if (canvasGroup.alpha <= 0)
            {
                _fadeOut = false;
            }
        }
    }

    private void SetAlbum(int id)
    {
        switch (id)
        {
            case 0: 
                albumCover.sprite = covers[0];
                albumName.text = albums[0];
                break;
            case 1: case 2:
                albumCover.sprite = covers[1];
                albumName.text = albums[1];
                break;
            case 3:
                albumCover.sprite = covers[2];
                albumName.text = albums[2];
                break;
            case 4: case 5: case 6:
                albumCover.sprite = covers[3];
                albumName.text = albums[3];
                break;
            case 7: case 8: case 9: case 10: case 11: case 12: case 13: case 14: case 15: case 16:
                albumCover.sprite = covers[4];
                albumName.text = albums[4];
                break;
            case 17: case 18:
                albumCover.sprite = covers[5];
                albumName.text = albums[5];
                break;
            case 19: case 20: case 21: case 22: case 23: case 24: case 25: case 26: case 27: case 28:
                albumCover.sprite = covers[6];
                albumName.text = albums[6];
                break;
            case 29: case 30: case 31: case 32:
                albumCover.sprite = covers[7];
                albumName.text = albums[7];
                break;
            case 33: case 34: case 35: case 36: case 37: case 38: case 39: case 40: case 41:
                albumCover.sprite = covers[8];
                albumName.text = albums[8];
                break;
            default: break;
        }
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(10f);
        _fadeOut = true;
    }
}
