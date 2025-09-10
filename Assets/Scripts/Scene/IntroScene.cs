using UnityEngine;

public class IntroScene : SceneBase
{
    
    public override void SceneLoading()
    {
        
    }

    public override void OnSceneEnter()
    {
        UIManager.Instance.GetUI<UIIntro>();
        
        // Init 함수 별도록 만들거나 Debug 찍는것도 방법
        // 지금처럼 해도 됨
        AudioManager.Instance.Init();
        AudioManager _audioManager = AudioManager.Instance;
    }

    public override void OnSceneExit()
    {
        
    }
}
