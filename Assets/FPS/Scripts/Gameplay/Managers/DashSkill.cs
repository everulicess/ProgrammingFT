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
        CharacterController m_Controller;

        [Header("Dash")]
        float dashTime = 0.5f;
        float dashSpeed = 6f;
        float dashDistance = 3f;


        [Header("Cooldown")]
        public float CurrentCooldown = 0;
        public float cooldown = 5f;
        PlayerInputHandler m_InputHandler;


        Vector3 moveDirection;

        //const float maxDashTime = 1f;
        //float dashStoppingSpeed = 0.1f;
        //float dashCooldown = 2f;

        //bool isDashing;

        //float currentCooldown;
        //float currentDashTime = maxDashTime;
        //float dashSpeed = 6f;


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
                Debug.Log($"dash pressed");
                Dash();
            }
            CurrentCooldown -= Time.deltaTime;
        }
        private void Dash()
        {
            if (CurrentCooldown > 0) return;
            else CurrentCooldown = cooldown;
            Vector3 horizontalVelocity = new Vector3(m_Controller.velocity.x, 0, m_Controller.velocity.z);
            Vector3 horizontalDirection = horizontalVelocity.normalized;

            moveDirection = horizontalDirection * dashDistance;
            StartCoroutine(Dashing());
            Debug.Log("Dash will be performed");
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

