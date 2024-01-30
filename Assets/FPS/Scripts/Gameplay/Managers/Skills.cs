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

        //public UnityAction<bool> OnUnlockDash;
        //public UnityAction<bool> OnAmmoIncreased;
        //public UnityAction<bool> OnHealthIncreased;
        //public UnityAction<bool> OnHealingIncreased;
        //public UnityAction<bool> OnSpeedIncreased;
        //public UnityAction<bool> OnDamageIncreased;

        MySkills skill;
        float amountToIncrease;

        public UnityAction<MySkills> OnSkillUpgraded;

        //bool healingUpgraded;
        DashSkill m_DashSkill;
        PlayerCharacterController m_PlayerController;
        Health m_PlayerHealth;
        [SerializeField] HealthPickup[] healingObjects;
        [SerializeField] ProjectileStandard blaster;
        [SerializeField] ProjectileStandard shotgun;
        [SerializeField] ProjectileStandard launcher;
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
        private void OnDestroy()
        {
            GameConstants.SkillData.TryGetValue(MySkills.Healing, out float healingValue);
            OnHealingUpgrade(-healingValue);
            //GameConstants.SkillData.TryGetValue(MySkills., out float value);
            EventManager.RemoveListener<SkillBuyEvent>(OnSkillBuy);
        }
        

        public void OnSkillBuy(SkillBuyEvent _event)
        {
            skill = _event.Skill;
            GameConstants.SkillData.TryGetValue(skill, out float value);
            amountToIncrease = value;

            Debug.Log($"name skill{skill} and value {value}");

            OnSkillUpgraded.Invoke(skill);
            switch (skill)
            {
                case MySkills.Health:
                    OnHealthUpgrade(amountToIncrease);
                    break;
                case MySkills.Healing:
                    OnHealingUpgrade(amountToIncrease);
                    break;
                case MySkills.LauncherDamage:
                    OnDamageUpgrade(launcher, amountToIncrease);
                    break;
                case MySkills.LauncherAmmo:
                    break;
                case MySkills.ShotgunDamage:
                    OnDamageUpgrade(shotgun, amountToIncrease);
                    break;
                case MySkills.ShotgunAmmo:
                    break;
                case MySkills.BlasterDamage:
                    OnDamageUpgrade(blaster, amountToIncrease);
                    break;
                case MySkills.BlasterAmmo:
                    break;
                case MySkills.Dash:
                    OnDashUnlocked();
                    break;
                case MySkills.Speed:
                    OnSpeedUpgrade(amountToIncrease);
                    break;
                default:
                    break;
            }
            //string nameSkill = _event.SkillName;
            //Debug.Log("this skill has been bought: " + nameSkill);
            //switch (nameSkill)
            //{
            //    case "Dash":
            //        OnDashUnlocked();
            //        ;break;
            //    case "Health":
            //        OnHealthUpgrade();
            //        ;break;
            //    case "Healing":
            //        OnHealingUpgrade();
            //        ; break;
            //    case "Speed":
            //        OnSpeedUpgrade();
            //        ; break;
            //    case "Ammo":
            //        OnAmmoIncreaseUpgrade();
            //        ; break;
            //    case "Damage":
            //        OnDamageUpgrade();
            //        ; break;
            //    default:
            //        break;
            //}
        }
        public void OnDamageUpgrade(ProjectileStandard projectile, float amount)
        {
            //OnDamageIncreased.Invoke(true);
            projectile.DamageUpgrade(amount);
        }
        public void OnHealthUpgrade(float amount)
        {
            //OnHealthIncreased.Invoke(true);
            m_PlayerHealth.HealthUpgrade(amount);

        }
        public void OnHealingUpgrade(float amount)
        {
            //OnHealingIncreased.Invoke(true);
            foreach (HealthPickup healthPickup in healingObjects)
            {
                healthPickup.HealthUpgrade(amount);
            }
        }
        public void OnAmmoIncreaseUpgrade(float amount)
        {
            //OnAmmoIncreased.Invoke(true);
        }
        public void OnDashUnlocked()
        {
            //OnUnlockDash.Invoke(true);
            m_DashSkill.enabled = true;
        }
        public void OnSpeedUpgrade(float amount)
        {
            //OnSpeedIncreased.Invoke(true);
            m_PlayerController.SpeedUpgrade(amount);
        }

    }
}
