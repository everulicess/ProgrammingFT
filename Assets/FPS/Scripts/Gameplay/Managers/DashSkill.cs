using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using System;

namespace Unity.FPS.Gameplay
{
    [RequireComponent(typeof(CharacterController))]
    [Serializable]
    public class DashSkill : MonoBehaviour
    {
        [Header("Dash")]
        float dashTime = 0.5f;
        float dashSpeed = 6f;
        float dashDistance = 3f;

        [Header("Cooldown")]
        public float CurrentCoolDown = 0;
        float cooldown = 5f;

        PlayerInputHandler m_InputHandler;
        CharacterController m_Controller;

        Vector3 moveDirection;

        private void Start()
        {
            m_InputHandler = GetComponent<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullGetComponent<PlayerInputHandler, DashSkill>(m_InputHandler,
                this, gameObject);

            m_Controller = GetComponent<CharacterController>();
            DebugUtility.HandleErrorIfNullGetComponent<CharacterController, DashSkill>(m_Controller,
                this, gameObject);
        }
        private void Update()
        {
            if (m_InputHandler.GetDashButtonDown())
            {
                Dash();
            }
            CoolDown();
        }

        private void CoolDown()
        {
            CurrentCoolDown -= Time.deltaTime;
        }

        private void Dash()
        {
            if (CurrentCoolDown > 0) return;
            else CurrentCoolDown = cooldown;
            Vector3 horizontalVelocity = new Vector3(m_Controller.velocity.x, 0, m_Controller.velocity.z);
            Vector3 horizontalDirection = horizontalVelocity.normalized;

            moveDirection = horizontalDirection * dashDistance;
            StartCoroutine(Dashing());
        }
        IEnumerator Dashing()
        {
            float startTime = Time.time;
            
            while (Time.time < startTime + dashTime)
            {
                m_Controller.Move(dashSpeed * Time.deltaTime * moveDirection);
                yield return null;
            }
        }
    }
}

