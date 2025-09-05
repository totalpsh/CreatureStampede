using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntro : UIBase
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() => SceneLoadManager.Instance.LoadScene(SceneType.Dungeon));
        settingButton.onClick.AddListener(OpenSetting);
        exitButton.onClick.AddListener(Application.Quit);   
    }

    private void OpenSetting()
    {
        UIManager.Instance.OpenUI<UISetting>();
    }
}
