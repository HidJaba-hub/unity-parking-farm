using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleSwitch : MonoBehaviour
{
    private Animator _animator;
    private Toggle _toggle;
    private static readonly int IsOn = Animator.StringToHash("IsOn");

    private void Awake()
    {
        TryGetComponent(out SoundSwitch soundSwitch);
        TryGetComponent(out _animator);
        TryGetComponent(out _toggle);
        _toggle.onValueChanged.AddListener(SetToggle);
        
        _toggle.isOn = soundSwitch.IsAudioOn();
        SetToggle(soundSwitch.IsAudioOn());
    }

    private void SetToggle(bool value)
    {
        _animator.SetBool(IsOn, value);
    }
}
