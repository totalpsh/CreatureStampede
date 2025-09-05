public class IntroScene : SceneBase
{
    
    public override void SceneLoading()
    {
        
    }

    public override void OnSceneEnter()
    {
        UIManager.Instance.GetUI<UIIntro>();
        AudioManager _audioManager = AudioManager.Instance;
    }

    public override void OnSceneExit()
    {
        
    }
}
