using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    /*
     TEMP PLAN - CHANGE IF YOU WISH
     on trigger enter
        - play light switch sound
        - activate big overlaying object with lightness (to make entire level bright)
        - some UI element saying "thanks for playing" or something polite
    */
    
    [Header("Object making entire level light again:")]
    [SerializeField] private GameObject _objectOfAbsoluteFuckingLightness;
    [Header("Canvas of victory text:")]
    [SerializeField] private GameObject _victoryCanvas;

    [Header("Lightswitch audio:")] 
    [SerializeField] private AudioData _lightSwitchSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SFXManager.PlaySoundAt(_lightSwitchSound, transform.position);
            _objectOfAbsoluteFuckingLightness.SetActive(true);
            _victoryCanvas.SetActive(true);
        }
    }
}
