using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDevScene : SceneBase
{
    StageManager _stageManager;
    public override void SceneLoading()
    {

    }

    public override void OnSceneEnter()
    {
        // 맵
        var map = ResourceManager.Instance.CreateMap<Stage>(Prefab.Stage);

        // 스테이지 매니저
        _stageManager = StageManager.Instance;
        _stageManager.InitStage(map);

        // 콜백 등록
        _stageManager.OnStageClear += GameClear;
        _stageManager.OnGameOver += GameOver;
    }


    public override void OnSceneExit()
    {
        _stageManager.OnStageClear -= GameClear;
        _stageManager.OnGameOver -= GameOver;

        _stageManager.Release();
    }


    void GameClear()
    {
        UIResult clear = UIManager.Instance.GetUI<UIResult>();
    }

    void GameOver()
    {
        UIResult fail = UIManager.Instance.GetUI<UIResult>();
    }

}
