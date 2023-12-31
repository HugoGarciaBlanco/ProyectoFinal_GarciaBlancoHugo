using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    
public AudioMixer audioMixer;

public void FullScreen(bool fullScreen) {
    Screen.fullScreen = fullScreen;
}

public void ChangeVolume(float volume) {
    audioMixer.SetFloat("Volume", volume);
}

public void ChangeQuality(int index) {
    QualitySettings.SetQualityLevel(index);
}

} 
