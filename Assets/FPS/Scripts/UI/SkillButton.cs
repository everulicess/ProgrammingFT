using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using Unity.FPS.UI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Unity.FPS.UI
{
    public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("References")]
        TextMeshProUGUI skillNameTextHolder;
        Button skillButton;
        
        [Header("Skill Settings")]
        [SerializeField] string skillName;
        [SerializeField] int skillCost;

        [Header("Skill Description References")]
        [SerializeField] GameObject skillExplanationObject;

        SkillTreeMenuManager m_skillTree;

        // Start is called before the first frame update
        void Start()
        {
            skillNameTextHolder = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            DebugUtility.HandleErrorIfNullFindObject<TextMeshProUGUI, NotificationHUDManager>(skillNameTextHolder, this);
            skillNameTextHolder.text = skillName;

            skillButton = this.gameObject.GetComponent<Button>();
            DebugUtility.HandleErrorIfNullFindObject<Button, NotificationHUDManager>(skillButton, this);
            skillButton.onClick.AddListener(ButtonClicked);
            

            m_skillTree = FindObjectOfType<SkillTreeMenuManager>();
            DebugUtility.HandleErrorIfNullFindObject<SkillTreeMenuManager, NotificationHUDManager>(m_skillTree, this);

            skillExplanationObject.SetActive(false);
        }
        
       
        private void ButtonClicked()
        {
            if (skillNameTextHolder.text == string.Empty) return;
        
            BuySkill($"{skillNameTextHolder.text}");


        }
        public void BuySkill(string skillName)
        {
            if (m_skillTree.skillPoints < skillCost) return;

            Debug.Log("click");
            SkillBuyEvent evt = Events.SkillBuyEvent;
            evt.SkillName = $"{skillName}";
            evt.SkillPrice = skillCost;
            EventManager.Broadcast(evt);

            skillButton.interactable = false;
        
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log($"hovering over button: {skillName}");
            ShowExplanation(true);
        }
        private void ShowExplanation(bool activate)
        {
            skillExplanationObject.SetActive(activate);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log($"hovering exit button: {skillName}");
            ShowExplanation(false);
        }
    }
}

