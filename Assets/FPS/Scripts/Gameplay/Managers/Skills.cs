using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Unity.FPS.Gameplay
{
    [RequireComponent(typeof(DashSkill), typeof(Health), typeof(PlayerCharacterController))]
    public class Skills : MonoBehaviour
    {
        //increase healing, increase health, increase speed, increase ammo, increase damage, having a dash with a cooldown

        [Header("Skill Names")]
        

        public UnityAction<bool> OnUnlockDash;
        public UnityAction<bool> OnAmmoIncreased;
        public UnityAction<bool> OnHealthIncreased;
        public UnityAction<bool> OnHealingIncreased;
        public UnityAction<bool> OnSpeedIncreased;
        public UnityAction<bool> OnDamageIncreased;
        bool healingUpgraded;
        DashSkill m_DashSkill;
        PlayerCharacterController m_PlayerController;
        Health m_PlayerHealth;
        private void Start()
        {
            m_DashSkill = GetComponent<DashSkill>();
            DebugUtility.HandleErrorIfNullGetComponent<DashSkill, Skills>(m_DashSkill,
                this, gameObject);
            m_DashSkill.enabled = false;

            m_PlayerController = GetComponent<PlayerCharacterController>();
            DebugUtility.HandleErrorIfNullGetComponent<PlayerCharacterController, Skills>(m_PlayerController,
                this, gameObject);

            m_PlayerHealth = GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, Skills>(m_PlayerHealth,
                this, gameObject);
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
                    HealthUpgrade();
                    ;break;
                case "Healing":
                    HealingUpgrade();
                    ; break;
                case "Speed":
                    OnSpeedUpgrade();
                    ; break;
                case "Ammo":
                    AmmoIncreaseUpgrade();
                    ; break;
                case "Damage":
                    OnDamageUpgrade();
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
        private void Update()
        {
            if (healingUpgraded)
            {
                foreach (HealthPickup healthPickup in FindObjectsOfType<HealthPickup>())
                {
                    healthPickup.HealAmount = 60;
                }
            }
        }
        public void OnDamageUpgrade()
        {
            OnDamageIncreased.Invoke(true);
        }
        public void HealthUpgrade()
        {
            OnHealthIncreased.Invoke(true);
            m_PlayerHealth.MaxHealth += 50f;
            Debug.Log(m_PlayerHealth.MaxHealth);
        }
        public void HealingUpgrade()
        {
            OnHealingIncreased.Invoke(true);
            healingUpgraded = true;
            
        }
        public void AmmoIncreaseUpgrade()
        {
            OnAmmoIncreased.Invoke(true);
        }
        public void OnDashUnlocked()
        {
            OnUnlockDash.Invoke(true);
            m_DashSkill.enabled = true;
        }
        public void OnSpeedUpgrade()
        {
            OnSpeedIncreased.Invoke(true);
            m_PlayerController.MaxSpeedOnGround += 10f;
        }
       
    }
}
