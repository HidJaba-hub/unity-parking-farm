using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSwitch : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Toggle _toggle;
    
    private void Awake()
    {
        TryGetComponent(out _toggle);
        _toggle.onValueChanged.AddListener(SetAudio);
    }
    private void SetAudio(bool value)
    {
        float soundDb = value ? 0 : -80f;
        audioMixer.SetFloat("MasterVolume", soundDb);
    }

    public bool IsAudioOn()
    {
        audioMixer.GetFloat("MasterVolume", out float value);
        return value == 0;
    }
}
