public class IntroScene : SceneBase
{
    public override void SceneLoading()
    {
        
    }

    public override void OnSceneEnter()
    {
        UIManager.Instance.GetUI<UIIntro>();
    }

    public override void OnSceneExit()
    {
        
    }
}
