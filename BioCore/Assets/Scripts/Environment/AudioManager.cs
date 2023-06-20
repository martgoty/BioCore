using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioMusic;
    [SerializeField] private AudioSource[] _audioSounds;

    private void Start() {
        if(!PlayerPrefs.HasKey("MusicVolume") && _audioMusic != null){
            _audioMusic.volume = 1;
        }
        if(!PlayerPrefs.HasKey("SoundsVolume") && _audioSounds.Length > 0){
            foreach(var audio in _audioSounds){
                audio.volume = 1;
            }
        }
        
    }

    private void Update() {
        if(_audioMusic != null)
            _audioMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
        if(_audioSounds.Length > 0){
            foreach(var audio in _audioSounds)
            audio.volume = PlayerPrefs.GetFloat("SoundsVolume");
        }
    }
}
