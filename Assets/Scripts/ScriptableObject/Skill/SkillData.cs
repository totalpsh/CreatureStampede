using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/New Skill")]
public class SkillData : ScriptableObject
{
    [SerializeField] private string skillName;
    //[SerializeField] private string skillIconPath;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private int skillLevel;
    [SerializeField] private string skillDescription;

    public string SkillName {  get { return skillName; } }
    //public string SkillIconPath { get { return skillIconPath; } }
    public Sprite SkillIcon { get { return skillIcon; } }
    public int SkillLevel { get { return skillLevel; } }
    public string SkillDescription { get { return skillDescription; } }
}
