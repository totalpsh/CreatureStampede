using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnMonster<Slime>();
        }
    }

    private Vector2 GetRandomSpawnPos()
    {
        float randomValue;
        float posX = Random.Range(-19f, 19f);
        float posY;
        randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            posX *= -1;
        }

        if ((-19f < posX && posX < -15f) || (15f < posX && posX < 19f))
        {
            posY = Random.value * 12f;
        }
        else
        {
            posY = Random.value * 4f + 8f;
        }

        randomValue = Random.value;
        if (randomValue < 0.5f)
        {
            posY *= -1;
        }
            
        return new Vector2(posX, posY);
    }

    private T SpawnMonster<T>() where T : MonsterBase
    {
        T monster = ResourceManager.Instance.CreateCharacter<T>(typeof(T).Name);
        monster.transform.position = GetRandomSpawnPos();

        return monster;
    }
}
