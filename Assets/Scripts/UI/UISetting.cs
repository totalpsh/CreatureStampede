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
    }

}
