using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 웨이브 데이터가 고정적
// 몬스터가 늘어날 경우는??
[CreateAssetMenu(fileName = "New Wave Data", menuName = "Monsters/Monster Wave Data")]
public class SO_MonsterWave : ScriptableObject
{
    [SerializeField] int playerLevel;
    [SerializeField] int slimeCount;
    [SerializeField] int goblinCount;
    [SerializeField] int trollCount;
    [SerializeField] float waveDelay;

    public int PlayerLevel => playerLevel;
    public int SlimeCount => slimeCount;
    public int GoblinCount => goblinCount;
    public int TrollCount => trollCount;
    public float WaveDelay => waveDelay;
}
