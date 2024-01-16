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

        public UnityAction<bool> OnUnlockDash;
        public UnityAction<bool> OnAmmoIncreased;
        public UnityAction<bool> OnHealthIncreased;
        public UnityAction<bool> OnHealingIncreased;
        private void Start()
        {
            //SkillTreeMenuManager skills = FindObjectOfType<SkillTreeMenuManager>();
            //DebugUtility.HandleErrorIfNullFindObject<SkillTreeMenuManager, Skills>(skills, this);
            //skills.OnUnlockDash += OnDashUnlocked;

            //skills.OnSkillBuy += OnSkillBuy;
            //Subscribe to events
            EventManager.AddListener<SkillBuyEvent>(OnSkillBuy);
        }
        public void OnSkillBuy(SkillBuyEvent _event)
        {
            string nameSkill = _event.SkillName;
            Debug.Log("this skill has been bought: " + nameSkill);
            switch (nameSkill)
            {
                case "Dash":
                    OnDashUnlocked();
                    ;break;
                case "Health":
                    AmmoIncreaseUpgrade();
                    ;break;
                case "Healing":
                    HealthUpgrade();
                    ; break;
                case "Speed":
                    
                    ; break;
                case "Ammo":

                    ; break;
                case "Damage":

                    ; break;
                default:
                    break;
            }
        }
        //public void OnSkillBuy(SkillBuyEvent _event)
        //{
        //    //_event.SkillName;
        //    switch (_event.SkillName)
        //    {
        //        case "Ammo Increase":
        //            Debug.LogWarning("AMMO INCREASE USING THE EVENT");
        //            ;break;
        //        default:
        //            break;
        //    }
        //}
        
        public void HealthUpgrade()
        {

        }
        public void HealingUpgrade()
        {

        }
        public void AmmoIncreaseUpgrade()
        {

        }
        public void OnDashUnlocked()
        {

            OnUnlockDash.Invoke(true);
        }
       
    }
}
