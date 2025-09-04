using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Stage _stage;
    public Stage Stage {  get { return _stage; } }
    private Player _player;

    public int Level { get; private set; } = 1;
    public int MaxExp { get; private set; } = 10;
    public int CurrentExp { get; set; } = 0;
    public int Score { get; set; } = 0;

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

    public event Action OnLevelExp;
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
            StageClear();
            OnStageClear?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            AddExp(10);
            AddScore(100);
            OnLevelExp?.Invoke();
            stageTime -= 100;
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

        //var playerHp = UIManager.Instance.GetUI<HpBar>();
        //playerHp.transform.SetParent(_player.transform, true);
        var playerInfo = UIManager.Instance.GetUI<UIInGame>();
        playerInfo.SetCharacter(_player);
    }

    private void SpawnVirtualCamera()
    {
        ResourceManager.Instance.Create<GameObject>(Path.Camera, Prefab.VirtualCamera);
    }

    private void LevelUp()
    {
        if(CurrentExp == MaxExp)
        {
            Level++;
            CurrentExp = 0;
            MaxExp = MaxExp + (Level * 10);
            UIManager.Instance.GetUI<UIInGame>().UpdateLevel();
            UIManager.Instance.GetUI<UIInGame>().UpdateExp();
        }
    }

    public void AddExp(int expValue)
    {
        CurrentExp += expValue;
        LevelUp();
    }

    public void AddScore(int scoreValue)
    {
        Score += scoreValue;
    }

    private void OnPlayerDie()
    {
        StopStage();
        OnGameOver?.Invoke();
    }

    private void StageClear()
    {
        UIResult ui = UIManager.Instance.GetUI<UIResult>();
        ui.ClearUI();
        Time.timeScale = 0;

    }
}
