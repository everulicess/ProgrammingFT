using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Unity.FPS.Gameplay
{

    public class Skills : MonoBehaviour
    {
        //increase healing, increase health, increase speed, increase ammo, increase damage, having a dash with a cooldown

        [Header("Skill Names")]
        [TextArea(1, 4)]
        public List<string> SkillNames = new();


        private void Start()
        {
            SkillTreeMenuManager skills = FindObjectOfType<SkillTreeMenuManager>();
            DebugUtility.HandleErrorIfNullFindObject<SkillTreeMenuManager, Skills>(skills, this);
            skills.OnUnlockDash += OnDashUnlocked;

            skills.OnSkillBuy += OnSkillBuy;
            //Subscribe to events
            EventManager.AddListener<PhysicalSkillUpgradedEvent>(OnPhysicalSkillUpgraded);
        }
        public void OnSkillBuy(string nameSkill)
        {
            Debug.Log("this skill has been bought: " + nameSkill);
            switch (nameSkill)
            {
                case "Dash":
                    Debug.Log("DASH HAS BEEN BOUGHT");
                    OnDashUnlocked(true);
                    ;break;
                case "Health Increase":
                    Debug.Log("Health Increase has been bought")
                    ;break;
                case "Healing Increase":
                    Debug.Log("Healing Increase has been bought")
                    ; break;
                case "Speed Increase":
                    Debug.Log("Speed Increase has been bought")
                    ; break;
                case "Ammo Increase":
                    Debug.Log("Ammo Increase has been bought")
                    ; break;
                case "Damage Increase":
                    Debug.Log("Healing Increase has been bought")
                    ; break;
                default:
                    break;
            }
        }
        public void OnPhysicalSkillUpgraded(PhysicalSkillUpgradedEvent _event)
        {
            //_event.SkillName;
            switch (_event.SkillName)
            {
                case "Ammo Increase":
                    Debug.LogWarning("AMMO INCREASE USING THE EVENT");
                    ;break;
                default:
                    break;
            }
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
        public void OnDashUnlocked(bool unlock)
        {
            
        }
       
    }
}
