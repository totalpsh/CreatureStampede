using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Dictionary<int, SO_MonsterWave> waveData = new();

    Transform playerTransform;
    
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnMonsterWave(waveData[1]);
        }
    }

    private void SpawnMonsterWave(SO_MonsterWave monsterWave)
    {
        for (int i = 0; i < monsterWave.SlimeCount; ++i)
        {
            SpawnMonster<Slime>();
        }

        for (int i = 0; i < monsterWave.GoblinCount; ++i)
        {
            SpawnMonster<Goblin>();
        }

        for (int i = 0; i < monsterWave.TrollCount; ++i)
        {
            SpawnMonster<Troll>();
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

    private T SpawnMonster<T>() where T : MonsterBase
    {
        T monster = ResourceManager.Instance.CreateCharacter<T>(typeof(T).Name);
        monster.transform.position = GetRandomSpawnPos();

        return monster;
    }

    IEnumerator SpawnMonsterDelay<T>(float delay) where T : MonsterBase
    {
        yield return new WaitForSeconds(delay);
    }
}
