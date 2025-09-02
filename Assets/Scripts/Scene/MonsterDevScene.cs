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

        ResourceManager.Instance.CreateCharacter<Troll>("Troll");
        ResourceManager.Instance.CreateCharacter<Slime>("Slime");
        ResourceManager.Instance.CreateCharacter<Goblin>("Goblin");
        ResourceManager.Instance.CreateCharacter<Bat>("Bat");


        // �ݹ� ���
        _stageManager.OnStageClear += GameClear;
        _stageManager.OnGameOver += GameOver;
    }


    public override void OnSceneExit()
    {
        _stageManager.OnStageClear += GameClear;
        _stageManager.OnGameOver += GameOver;

        _stageManager.Release();
    }


    void GameClear()
    {
        //UIManager.Instance.GetUI<UIGameClear>();
    }

    void GameOver()
    {
        //UIManager.Instance.GetUI<UIGameOver>();
    }

}
