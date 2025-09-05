using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UIBase
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Button closeButton;
    private void Awake()
    {
        closeButton.onClick.AddListener(CloseUI);
        bgmSlider.value = AudioManager.Instance.BgmVolume;
        sfxSlider.value = AudioManager.Instance.SfxVolume;

    }

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.BgmVolume = value;
        });
        sfxSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.SfxVolume = value;
        });
    }

}
