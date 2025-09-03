using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    private Stage _stage;
    private Player _player;

    private int _monsterCount;
    public int MonsterCount {
        get => _monsterCount;
        set
        {
            _monsterCount = value;
            OnMonsterCountChanged?.Invoke(_monsterCount);
        }
    }

    private float stageTime = 600f;
    public float StageTime { get { return stageTime; } }
    private bool isRunning = false;
    
    public event Action OnStageClear;
    public event Action OnGameOver;
    public event Action<int> OnMonsterCountChanged;

    private void Update()
    {
        if (!isRunning) return;

        if(stageTime > 0f)
        {
            stageTime -= Time.deltaTime;
        }
        else
        {
            stageTime = 0;
            isRunning = false;
            // 게임 클리어
            OnStageClear?.Invoke();
        }
    }

    public void InitStage(Stage stage)
    {
        // 맵 생성
        _stage = stage;
        SpawnPlayer();
        SpawnVirtualCamera();
        isRunning = true;
    }

    public void StopStage()
    {
        _player.Stop();
    }
    
    private void SpawnPlayer()
    {
        var playerPos = transform; // 플레이어 시작 위치   

        _player = PlayerManager.Instance.Player;
        _player.OnCharacterDie += OnPlayerDie;
        _player.SetPosition(playerPos);
        _player.Init();

        var playerInfo = UIManager.Instance.GetUI<UIInGame>();
        //playerInfo.SetCharacter(_player);
    }

    private void SpawnVirtualCamera()
    {
        ResourceManager.Instance.Create<GameObject>(Path.Camera, Prefab.VirtualCamera);
    }


    private void OnPlayerDie()
    {
        StopStage();
        OnGameOver?.Invoke();
    }

    private void StageTimeOut()
    {

    }
}
