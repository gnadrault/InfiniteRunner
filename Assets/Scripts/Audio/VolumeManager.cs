using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class VolumeManager : GameBehavior
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private TextMeshProUGUI musicValue;
        [SerializeField] private TextMeshProUGUI sfxValue;

        private void Start()
        {
            OnSfxChanged(musicSlider.value);
            OnMusicChanged(musicSlider.value);
        }

        public void OnMusicChanged(float value)
        {
            musicValue.text = value.ToString("F0");
        }
        
        public void OnSfxChanged(float value)
        {
            sfxValue.text = value.ToString("F0");
        }
    }
}
