using System;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Stage _stage;
    public Stage Stage {  get { return _stage; } }
    private Player _player;

    private int level;
    public int Level { get => level; set { level = value; OnLevelChanged?.Invoke(level); } }
    private int maxExp;
    public int MaxExp { get => maxExp; set { maxExp = value; OnMaxExpChange?.Invoke(maxExp); } }
    private int currentExp;
    public int CurrentExp 
    { 
        get => currentExp; 
        set 
        { 
            currentExp = value;
            if (currentExp >= maxExp)
            {
                PlayerLevelUp();
            }
            OnExpChange?.Invoke(currentExp);
            
        } 
    }
    private int score;
    public int Score { get => score; set { score = value; OnScoreChange?.Invoke(score); } }

    public event Action<int /*level*/> OnLevelChanged;
    public event Action<int /*exp*/> OnExpChange;
    public event Action<int /*maxExp*/> OnMaxExpChange;
    public event Action<int /*score*/> OnScoreChange;

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

    private void Start()
    {
        Level = 1;
        MaxExp = 10;
        CurrentExp = 0;
        Score = 0;
        
    }

    private void PlayerLevelUp()
    {
        Level++;
        currentExp -= maxExp;

        MaxExp = MaxExp + (Level * 10);

    }

    public void InitStage(Stage stage)
    {
        // 맵 생성
        _stage = stage;
        SpawnPlayer();
        SpawnVirtualCamera();
        isRunning = true;

        level = 1;
        currentExp = 0;
        score = 0;
        maxExp = 10;
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
        if(CurrentExp >= MaxExp)
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
