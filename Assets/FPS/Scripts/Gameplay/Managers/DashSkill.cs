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
        //public Transform orientation;
        //public Transform playerCam;
        CharacterController m_Controller;
        //PlayerCharacterController m_PlayerController;
        
        [Header("Dash")]
        //public float dashForce;
        //public float dashUpwardForce;
        //[SerializeField] float dashDuration = 3f;
        //float dashTime;
        
        //[SerializeField] float dashCooldownTimer;
        
        PlayerInputHandler m_InputHandler;

        Vector3 moveDirection;

        const float maxDashTime = 1f;
        float dashStoppingSpeed = 0.1f;
        float dashDistance = 5f;
        float dashCooldown = 2f;

        bool isDashing;

        float currentCooldown;
        float currentDashTime = maxDashTime;
        float dashSpeed = 6f;


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
            
            if ( m_InputHandler.GetDashButtonDown())
            {
                Dash();
            }
            ResetDash();
            if (currentCooldown>0)
            {
                currentCooldown -= Time.deltaTime;
            }
        }
        private void Dash()
        {
            if (currentCooldown > 0) return;
            else currentCooldown = dashCooldown;
            Debug.Log("DASHING RIGHT NOW");
            isDashing = true;
            currentDashTime = 0;

            Vector3 horizontalVelocity = new Vector3(m_Controller.velocity.x, 0, m_Controller.velocity.z);
            Vector3 horizontalDirection = horizontalVelocity.normalized;

            moveDirection = horizontalDirection * dashDistance;
        }

        private void ResetDash()
        {
            if (!isDashing) return;
            Debug.Log("DASH RESET");

            if (currentDashTime < maxDashTime)
            {
                currentDashTime += dashStoppingSpeed;
            }
            else
            {
                isDashing = false;
                //currentCooldown = dashCooldown;
                moveDirection = Vector3.zero;
            }
            m_Controller.Move(moveDirection * Time.deltaTime * dashSpeed);

            
        }
    }
}

