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
        // ��
        var map = ResourceManager.Instance.CreateMap<Stage>(Prefab.Stage);

        // �������� �Ŵ���
        _stageManager = StageManager.Instance;
        _stageManager.InitStage(map);

        // �ݹ� ���
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
