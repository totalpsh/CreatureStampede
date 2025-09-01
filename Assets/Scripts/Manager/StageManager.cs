using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : Singleton<StageManager>
{
    //private Stage _stage;
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
    
    public event Action OnStageClear;
    public event Action OnGameOver;
    public event Action<int> OnMonsterCountChanged;

    public void InitStage(/*Stage stage*/)
    {
        // 맵 생성
        SpawnPlayer();
        SpawnVirtualCamera();
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


    private void OnPlayerDie(Player player)
    {
        StopStage();
        OnGameOver?.Invoke();
    }
}
