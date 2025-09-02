using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button titleButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(OnContinue);
        restartButton.onClick.AddListener(OnRestart);
        titleButton.onClick.AddListener(OnTitle);
    }

    public void OnContinue()
    {
        Debug.Log("����ϱ�");
    }

    public void OnRestart()
    {
        Debug.Log("�ٽ��ϱ�");

    }

    public void OnTitle()
    {
        Debug.Log("Ÿ��Ʋ��");

    }

}
