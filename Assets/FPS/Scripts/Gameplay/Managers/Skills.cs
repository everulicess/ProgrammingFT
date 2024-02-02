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
        MySkills skill;
        float amountToIncrease;

        public UnityAction<MySkills> OnSkillUpgraded;

        DashSkill m_DashSkill;
        PlayerCharacterController m_PlayerController;
        Health m_PlayerHealth;

        [Header("Healing Pickups Prefabs")]
        [SerializeField] List<HealthPickup> healingObjects;
        bool isHealingChanged = false;

        [Header("Projectiles Prefabs")]
        [SerializeField] ProjectileStandard blaster;
        bool isAmmoBlasterChanged = false;
        [SerializeField] ProjectileStandard shotgun;
        bool isAmmoShotgunChanged = false;
        [SerializeField] ProjectileStandard launcher;
        bool isAmmoLauncherChanged = false;

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

            EventManager.AddListener<SkillBuyEvent>(OnSkillBuy);
        }
        private void OnDestroy()
        {
            if (isHealingChanged)
            {
                GameConstants.SkillData.TryGetValue(MySkills.Healing, out float value);
                HealingUpgrade(-value);
            }
            if (isAmmoShotgunChanged)
            {
                GameConstants.SkillData.TryGetValue(MySkills.ShotgunAmmo, out float value);
                AmmoUpgrade("Shotgun", -value);
            }
            if (isAmmoLauncherChanged)
            {
                GameConstants.SkillData.TryGetValue(MySkills.LauncherAmmo, out float value);
                AmmoUpgrade("Disc Launcher", -value);
            }
            if (isAmmoBlasterChanged)
            {
                GameConstants.SkillData.TryGetValue(MySkills.BlasterAmmo, out float value);
                AmmoUpgrade("Blaster",-value);
            }
            
            EventManager.RemoveListener<SkillBuyEvent>(OnSkillBuy);
        }
        public void OnSkillBuy(SkillBuyEvent _event)
        {
            skill = _event.Skill;
            GameConstants.SkillData.TryGetValue(skill, out float value);
            amountToIncrease = value;

            OnSkillUpgraded.Invoke(skill);
            switch (skill)
            {
                case MySkills.Health:
                    HealthUpgrade(amountToIncrease);
                    break;
                case MySkills.Healing:
                    HealingUpgrade(amountToIncrease);
                    isHealingChanged = true;
                    break;
                case MySkills.LauncherDamage:
                    DamageUpgrade(launcher, amountToIncrease);
                    break;
                case MySkills.LauncherAmmo:
                    AmmoUpgrade("Disc Launcher", amountToIncrease);
                    isAmmoLauncherChanged = true;
                    break;
                case MySkills.ShotgunDamage:
                    DamageUpgrade(shotgun, amountToIncrease);
                    break;
                case MySkills.ShotgunAmmo:
                    AmmoUpgrade("Shotgun", amountToIncrease);
                    isAmmoShotgunChanged = true;
                    break;
                case MySkills.BlasterDamage:
                    DamageUpgrade(blaster, amountToIncrease);
                    break;
                case MySkills.BlasterAmmo:
                    AmmoUpgrade("Blaster", amountToIncrease);
                    isAmmoBlasterChanged = true;
                    break;
                case MySkills.Dash:
                    DashUnlocked();
                    break;
                case MySkills.Speed:
                    SpeedUpgrade(amountToIncrease);
                    break;
                default:
                    break;
            }
        }
        public void DamageUpgrade(ProjectileStandard projectile, float amount)
        {
            projectile.DamageUpgrade(amount);
        }
        public void AmmoUpgrade(string weaponName, float amount)
        {
            foreach (WeaponController weapon in FindObjectsOfType<WeaponController>())
            {
                if (weapon.WeaponName == weaponName)
                {
                    weapon.AmmoUpgrade(amount);
                }
            }
        }
        public void HealthUpgrade(float amount)
        {
            m_PlayerHealth.HealthUpgrade(amount);
        }
        public void HealingUpgrade(float amount)
        {
            m_PlayerHealth.HealingUpgrade(amount);
        }
        public void DashUnlocked()
        {
            m_DashSkill.enabled = true;
        }
        public void SpeedUpgrade(float amount)
        {
            m_PlayerController.SpeedUpgrade(amount);
        }
    }
}
