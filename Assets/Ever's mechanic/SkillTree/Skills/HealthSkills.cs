using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSkills : MonoBehaviour
{
    //increase healing, increase health, increase speed, increase ammo, increase damage, having a dash with a cooldown

    [Header("Skill Names")]
    [TextArea(1, 4)]
    public List<string> SkillNames = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateSkill(string name)
    {

    }
    public void HealthUpgrade()
    {

    }
    public void HealingUpgrade()
    {

    }
    public void AmmoIncreaseUpgrade()
    {
        
    }
    public void DamageUpgrade()
    {

    }
    public void DashUnlocked()
    {
        
    }
}
