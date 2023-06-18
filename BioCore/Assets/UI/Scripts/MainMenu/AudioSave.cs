using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSave : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSounds;
    private float volumeMusic;
    private float volumeSounds;

    private void Start() {
        volumeMusic = _sliderMusic.value;
        volumeSounds = _sliderSounds.value;
        if(!PlayerPrefs.HasKey("MusicVolume")){
            _sliderMusic.value = 1;
        }
        else{
            _sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        }

        if(!PlayerPrefs.HasKey("SoundsVolume")){
            _sliderSounds.value = 1;
        }
        else{
            _sliderSounds.value = PlayerPrefs.GetFloat("SoundsVolume");
        }
    }

    private void Update() {
        if(volumeMusic != _sliderMusic.value){
            PlayerPrefs.SetFloat("MusicVolume", _sliderMusic.value);
            PlayerPrefs.Save();
            volumeMusic = _sliderMusic.value;
        }
        
        if(volumeSounds != _sliderSounds.value){
            PlayerPrefs.SetFloat("SoundsVolume", _sliderSounds.value);
            PlayerPrefs.Save();
            volumeSounds = _sliderSounds.value;
        }
    }
}
