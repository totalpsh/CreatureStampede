using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Stage : MonoBehaviour
{
    Dictionary<int, SO_MonsterWave> waveData = new();

    public Dictionary<string, ObjectPool<GameObject>> pools { get; private set; } = new();
    
    float lastWaveSpawnTime;
    float currentWaveDelay;

    float lastBatsSpawnTime;
    float batsSpawnDelay = 20f;

    int playerLevel;

    Transform poolRoot;
    Transform playerTransform;
    SO_MonsterWave currentWaveData;

    private void Awake()
    {
        poolRoot = new GameObject("@MonsterPoolRoot").transform;
        SO_MonsterWave[] monsterWaves = Resources.LoadAll<SO_MonsterWave>(Path.Data + "MonsterWave");

        for (int i = 0; i < monsterWaves.Length; i++)
        {
            waveData.Add(monsterWaves[i].PlayerLevel, monsterWaves[i]);
        }

        GameObject[] monsterPrefabs = Resources.LoadAll<GameObject>(Path.Monster);

        for (int i = 0; i < monsterPrefabs.Length; ++i)
        {
            string monsterName = monsterPrefabs[i].name;

            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
                createFunc: () =>
                {
                    GameObject obj = ResourceManager.Instance.CreateMonster<GameObject>(monsterName, poolRoot);
                    return obj;
                },
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                maxSize: 100
                );

            pools.TryAdd(monsterName, pool);
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

        if (Input.GetKeyDown(KeyCode.E))
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

        StartCoroutine(SpawnBatsCoroutine(0.1f));
    }

    IEnumerator SpawnBatsCoroutine(float delay)
    {
        Vector2 pos = GetRandomSpawnPos();
        int batCount = 5;
        for (int i = 0; i < batCount; ++i)
        {
            Bat bat = SpawnMonster<Bat>();
            bat.transform.position = pos;
            yield return new WaitForSeconds(delay);
        }
    }

    private T SpawnMonsterRandomPos<T>() where T : MonsterBase
    {
        //T monster = ResourceManager.Instance.CreateMonster<T>(typeof(T).Name);
        string monsterName = typeof(T).Name;
        var monster = pools[monsterName].Get();
        monster.transform.position = GetRandomSpawnPos();

        return monster.GetComponent<T>();
    }

    private T SpawnMonster<T>() where T : MonsterBase
    {
        // T monster = ResourceManager.Instance.CreateMonster<T>(typeof(T).Name);
        string monsterName = typeof(T).Name;
        var monster = pools[monsterName].Get();
        return monster.GetComponent<T>();

    }

    IEnumerator SpawnMonsterDelay<T>(float delay) where T : MonsterBase
    {
        yield return new WaitForSeconds(delay);
    }
}
