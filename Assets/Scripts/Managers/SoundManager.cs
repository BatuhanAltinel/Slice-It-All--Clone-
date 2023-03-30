using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _cutClip;
    [SerializeField] AudioClip _pushBackClip;
    [SerializeField] AudioClip _finisClip;

    [SerializeField] AudioClip[] _swishClips;

    public bool IsAudioOn = true;

    void OnEnable()
    {
        EventManager.OnTap += PlaySwish;
        EventManager.OnSlice += PlayCut;
        EventManager.OnFinish += PlayFinish;
        EventManager.OnPushBack += PlayPushback;
    }

    void OnDisable()
    {
        EventManager.OnSlice -= PlayCut;
        EventManager.OnTap -= PlaySwish;
        EventManager.OnFinish -= PlayFinish;
        EventManager.OnPushBack -= PlayPushback;
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void PlaySwish()
    {
        if (IsAudioOn)
            _audioSource.PlayOneShot(_swishClips[Random.Range(0,_swishClips.Length)]);
    }

    void PlayCut()
    {
        if (IsAudioOn)
            _audioSource.PlayOneShot(_cutClip);
    }
    void PlayFinish()
    {
        if (IsAudioOn)
            _audioSource.PlayOneShot(_finisClip);
    }

    void PlayPushback()
    {
        if(IsAudioOn)
            _audioSource.PlayOneShot(_pushBackClip);
    }
}
