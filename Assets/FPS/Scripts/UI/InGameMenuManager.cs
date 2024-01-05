using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class InGameMenuManager : MonoBehaviour
    {
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        [Tooltip("Slider component for look sensitivity")]
        public Slider LookSensitivitySlider;

        [Tooltip("Toggle component for shadows")]
        public Toggle ShadowsToggle;

        [Tooltip("Toggle component for invincibility")]
        public Toggle InvincibilityToggle;

        [Tooltip("Toggle component for framerate display")]
        public Toggle FramerateToggle;

        [Tooltip("GameObject for the controls")]
        public GameObject ControlImage;

        //skill tree
        [Tooltip("Root GameObject of the Skill Tree used to toggle its activation")]
        public GameObject SkillTree;

        [Tooltip("GameObject for the Health Skills")]
        public GameObject HealthSkillsImage;

        [Tooltip("GameObject for the Movement Skills")]
        public GameObject MovementSkillsImage;

        [Tooltip("GameObject for the Weapons Skills")]
        public GameObject WeaponsSkillsImage;

        PlayerInputHandler m_PlayerInputsHandler;
        Health m_PlayerHealth;
        FramerateCounter m_FramerateCounter;

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerInputHandler, InGameMenuManager>(m_PlayerInputsHandler,
                this);

            m_PlayerHealth = m_PlayerInputsHandler.GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, InGameMenuManager>(m_PlayerHealth, this, gameObject);

            m_FramerateCounter = FindObjectOfType<FramerateCounter>();
            DebugUtility.HandleErrorIfNullFindObject<FramerateCounter, InGameMenuManager>(m_FramerateCounter, this);

            MenuRoot.SetActive(false);
            SkillTree.SetActive(false);

            LookSensitivitySlider.value = m_PlayerInputsHandler.LookSensitivity;
            LookSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);

            ShadowsToggle.isOn = QualitySettings.shadows != ShadowQuality.Disable;
            ShadowsToggle.onValueChanged.AddListener(OnShadowsChanged);

            InvincibilityToggle.isOn = m_PlayerHealth.Invincible;
            InvincibilityToggle.onValueChanged.AddListener(OnInvincibilityChanged);

            FramerateToggle.isOn = m_FramerateCounter.UIText.gameObject.activeSelf;
            FramerateToggle.onValueChanged.AddListener(OnFramerateCounterChanged);
        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && !SkillTree.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
                || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                if (ControlImage.activeSelf)
                {
                    ControlImage.SetActive(false);
                    return;
                }

                SetPauseMenuActivation(!MenuRoot.activeSelf);

            }

            //------------------SkillTree--------------------------
            if (Input.GetButtonDown(GameConstants.k_ButtonNameSkillTree)
                || (SkillTree.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                if (HealthSkillsImage.activeSelf)
                {
                    HealthSkillsImage.SetActive(false);
                    return;
                }
                if ( MovementSkillsImage.activeSelf)
                {
                    MovementSkillsImage.SetActive(false);
                    return;
                }
                if (WeaponsSkillsImage.activeSelf)
                {
                    WeaponsSkillsImage.SetActive(false);
                    return;
                }
                Debug.Log("OPEN SKILL TREE");
                SetSkillTreeActivation(!SkillTree.activeSelf);
            }

            if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    LookSensitivitySlider.Select();
                }
            }
        }

        public void ClosePauseMenu()
        {
            SetPauseMenuActivation(false);
        }
       
        void SetPauseMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);
            if (MenuRoot.activeSelf)
            {
                SkillTree.SetActive(!active);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }

        void OnMouseSensitivityChanged(float newValue)
        {
            m_PlayerInputsHandler.LookSensitivity = newValue;
        }

        void OnShadowsChanged(bool newValue)
        {
            QualitySettings.shadows = newValue ? ShadowQuality.All : ShadowQuality.Disable;
        }

        void OnInvincibilityChanged(bool newValue)
        {
            m_PlayerHealth.Invincible = newValue;
        }

        void OnFramerateCounterChanged(bool newValue)
        {
            m_FramerateCounter.UIText.gameObject.SetActive(newValue);
        }

        public void OnShowControlButtonClicked(bool show)
        {
            ControlImage.SetActive(show);
        }

        //------------------------------------------skill tree----------------------------
        public void CloseSkillTree()
        {
            SetSkillTreeActivation(false);
        }
        void SetSkillTreeActivation(bool active)
        {
            SkillTree.SetActive(active);


            if (SkillTree.activeSelf)
            {
                MenuRoot.SetActive(!active);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }
        public void OnShowHealthSkillsButtonClicked(bool show)
        {
            HealthSkillsImage.SetActive(show);
            MovementSkillsImage.SetActive(!show);
            WeaponsSkillsImage.SetActive(!show);
        }
        public void OnShowMovementSkillsButtonClicked(bool show)
        {
            MovementSkillsImage.SetActive(show);
            WeaponsSkillsImage.SetActive(!show);
            HealthSkillsImage.SetActive(!show);


        }
        public void OnShowWeaponsSkillsButtonClicked(bool show)
        {
            WeaponsSkillsImage.SetActive(show);
            HealthSkillsImage.SetActive(!show);
            MovementSkillsImage.SetActive(!show);
        }
    }
}