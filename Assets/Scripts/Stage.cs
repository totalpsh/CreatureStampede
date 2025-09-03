using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Dictionary<int, SO_MonsterWave> waveData = new();
    float lastWaveSpawnTime;
    float currentWaveDelay;

    float lastBatsSpawnTime;
    float batsSpawnDelay = 20f;

    int playerLevel;

    Transform playerTransform;
    SO_MonsterWave currentWaveData;

    private void Awake()
    {
        SO_MonsterWave[] monsterWaves = Resources.LoadAll<SO_MonsterWave>(Path.Data + "MonsterWave");

        Debug.Log(monsterWaves.Length);

        for (int i = 0; i < monsterWaves.Length; i++)
        {
            waveData.Add(monsterWaves[i].PlayerLevel, monsterWaves[i]);
        }
    }

    private void Start()
    {
        playerTransform = PlayerManager.Instance.Player.transform;
        currentWaveData = waveData[1];
        currentWaveDelay = currentWaveData.WaveDelay;

        SpawnMonsterWave(currentWaveData);

    }

    private void Update()
    {
        if (Time.time - lastWaveSpawnTime > currentWaveDelay)
        {
            SpawnMonsterWave(currentWaveData);
        }
        if (Time.time - lastBatsSpawnTime > batsSpawnDelay)
        {
            SpawnBats();
        }
    }

    // playerLevelUp 할때 호출
    public void OnPlayerLevelUp(int playerLevel)
    {
        if (waveData.ContainsKey(playerLevel))
        {
            currentWaveData = waveData[playerLevel];
        }
        currentWaveDelay = currentWaveData.WaveDelay;
    }

    private void SpawnMonsterWave(SO_MonsterWave monsterWave)
    {
        lastWaveSpawnTime = Time.time;

        for (int i = 0; i < monsterWave.SlimeCount; ++i)
        {
            SpawnMonsterRandomPos<Slime>();
        }

        for (int i = 0; i < monsterWave.GoblinCount; ++i)
        {
            SpawnMonsterRandomPos<Goblin>();
        }

        for (int i = 0; i < monsterWave.TrollCount; ++i)
        {
            SpawnMonsterRandomPos<Troll>();
        }
    }

    private Vector2 GetRandomSpawnPos()
    {
        float posX = Random.Range(-19f, 19f);
        float posY;

        if ((-19f < posX && posX < -15f) || (15f < posX && posX < 19f))
        {
            posY = Random.value * 12f;
        }
        else
        {
            posY = Random.value * 4f + 8f;
        }

        if (Random.value < 0.5f)
        {
            posY *= -1;
        }

        posX += playerTransform.position.x;
        posY += playerTransform.position.y;
            
        return new Vector2(posX, posY);
    }

    public void SpawnBats()
    {
        lastBatsSpawnTime = Time.time;

        Vector2 pos = GetRandomSpawnPos();
        int batCount = 5;
        for (int i = 0; i < batCount; ++i)
        {
            Bat bat = SpawnMonster<Bat>();
            bat.transform.position = pos;
        }
    }

    private T SpawnMonsterRandomPos<T>() where T : MonsterBase
    {
        T monster = ResourceManager.Instance.CreateCharacter<T>(typeof(T).Name);
        monster.transform.position = GetRandomSpawnPos();

        return monster;
    }

    private T SpawnMonster<T>() where T : MonsterBase
    {
        T monster = ResourceManager.Instance.CreateCharacter<T>(typeof(T).Name);

        return monster;
    }

    IEnumerator SpawnMonsterDelay<T>(float delay) where T : MonsterBase
    {
        yield return new WaitForSeconds(delay);
    }
}
