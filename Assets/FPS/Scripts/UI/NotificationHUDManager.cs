using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Unity.FPS.UI
{
    public class NotificationHUDManager : MonoBehaviour
    {
        [Tooltip("UI panel containing the layoutGroup for displaying notifications")]
        public RectTransform NotificationPanel;

        [Tooltip("Prefab for the notifications")]
        public GameObject NotificationPrefab;
        
        void Awake()
        {
            PlayerWeaponsManager playerWeaponsManager = FindObjectOfType<PlayerWeaponsManager>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, NotificationHUDManager>(playerWeaponsManager,
                this);
            playerWeaponsManager.OnAddedWeapon += OnPickupWeapon;

            Jetpack jetpack = FindObjectOfType<Jetpack>();
            DebugUtility.HandleErrorIfNullFindObject<Jetpack, NotificationHUDManager>(jetpack, this);
            jetpack.OnUnlockJetpack += OnUnlockJetpack;

            //Skills unlocking
            //Dash
            Skills skills = FindObjectOfType<Skills>();
            DebugUtility.HandleErrorIfNullFindObject<Skills, NotificationHUDManager>(skills, this);
            skills.OnUnlockDash += OnDashUnlocked;

            //Ammo Increasing
            skills.OnAmmoIncreased += OnAmmoIncreased;

            skills.OnHealingIncreased += OnHealingIncreased;

            skills.OnDamageIncreased += OnDamageIncreased;

            skills.OnHealthIncreased += OnHealthIncreased;

            skills.OnSpeedIncreased += OnSpeedIncreased;
            
            EventManager.AddListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        }

        void OnObjectiveUpdateEvent(ObjectiveUpdateEvent evt)
        {
            if (!string.IsNullOrEmpty(evt.NotificationText))
                CreateNotification(evt.NotificationText);
        }

        void OnPickupWeapon(WeaponController weaponController, int index)
        {
            if (index != 0)
                CreateNotification("Picked up weapon : " + weaponController.WeaponName);
        }

        void OnUnlockJetpack(bool unlock)
        {
            CreateNotification("Jetpack unlocked");
        }

        //On Dash Unlocked
        void OnDashUnlocked(bool unlock)
        {
            CreateNotification("Dash Unlocked");
        }
        void OnAmmoIncreased(bool unlock)
        {
            CreateNotification("Ammo Increased");
        } 
        void OnHealingIncreased(bool unlock)
        {
            CreateNotification("Healing Increased");
        }
        void OnDamageIncreased(bool unlock)
        {
            CreateNotification("Damage Increased");
        }
        void OnHealthIncreased(bool unlock)
        {
            CreateNotification("Health Increased");
        }
        void OnSpeedIncreased(bool unlock)
        {
            CreateNotification("Speed Increased");
        }

        public void CreateNotification(string text)
        {
            GameObject notificationInstance = Instantiate(NotificationPrefab, NotificationPanel);
            notificationInstance.transform.SetSiblingIndex(0);

            NotificationToast toast = notificationInstance.GetComponent<NotificationToast>();
            if (toast)
            {
                toast.Initialize(text);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        }
    }
}