using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class DashCounter : MonoBehaviour
    {
        [Tooltip("Image component representing dash cooldown")]
        public Image DashFillImage;

        [Tooltip("Canvas group that contains the whole UI for the dash")]
        public CanvasGroup MainCanvasGroup;

        [Tooltip("Component to animate the color when empty or full")]
        public FillBarColorChange FillBarColorChange;

        DashSkill m_Dash;

        void Awake()
        {
            m_Dash = FindObjectOfType<DashSkill>();
            DebugUtility.HandleErrorIfNullFindObject<DashSkill, DashCounter>(m_Dash, this);

            FillBarColorChange.Initialize(0f, -5f);
        }

        void Update()
        {
            MainCanvasGroup.gameObject.SetActive(m_Dash.enabled == true);

            if (m_Dash.enabled == true)
            {
                DashFillImage.fillAmount = -m_Dash.CurrentCooldown;
                //FillBarColorChange.UpdateVisual(m_Jetpack.CurrentFillRatio);
            }
        }
    }
}