using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimSet
{
    [SerializeField] AnimationClip left;
    [SerializeField] AnimationClip right;
    [SerializeField] AnimationClip down;
    [SerializeField] AnimationClip up;

    public AnimationClip Left => left;
    public AnimationClip Right => right;
    public AnimationClip Down => down;
    public AnimationClip Up => up;
}

[CreateAssetMenu(fileName = "New Animation Set", menuName = "Monsters/Animation Set")]
public class SO_MonsterAnimSet : ScriptableObject
{

    [SerializeField] AnimSet idleAnimSet;
    [SerializeField] AnimSet moveAnimSet;
    [SerializeField] AnimSet attackAnimSet;
    [SerializeField] AnimSet hitAnimSet;
    [SerializeField] AnimSet dieAnimset;

    public AnimSet IdleAnimSet => idleAnimSet;
    public AnimSet MoveAnimSet => moveAnimSet;
    public AnimSet AttackAnimSet => attackAnimSet;
    public AnimSet HitAnimSet => hitAnimSet;
    public AnimSet DieAnimSet => dieAnimset;
}
