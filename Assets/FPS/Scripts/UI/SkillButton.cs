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
        [SerializeField] MySkills skill;
        [SerializeField] int skillCost;
        [SerializeField] string skillDescription;

        [Header("Skill Description References")]
        [SerializeField] GameObject skillExplanationObject;
        [SerializeField] TextMeshProUGUI skillExplanationTextHolder;

        SkillTreeMenuManager m_SkillTree;

        // Start is called before the first frame update
        void Start()
        {
            skillNameTextHolder = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            DebugUtility.HandleErrorIfNullFindObject<TextMeshProUGUI, NotificationHUDManager>(skillNameTextHolder, this);
            skillNameTextHolder.text = skill.ToString();

            skillButton = this.gameObject.GetComponent<Button>();
            DebugUtility.HandleErrorIfNullFindObject<Button, NotificationHUDManager>(skillButton, this);
            skillButton.onClick.AddListener(ButtonClicked);
            

            m_SkillTree = FindObjectOfType<SkillTreeMenuManager>();
            DebugUtility.HandleErrorIfNullFindObject<SkillTreeMenuManager, NotificationHUDManager>(m_SkillTree, this);

            skillExplanationObject.SetActive(false);
            skillExplanationTextHolder.text = $"{skillDescription}\n Price: {skillCost}";
        }
        
       
        private void ButtonClicked()
        {
            if (skillNameTextHolder.text == string.Empty) return;
        
            OnBuySkill(skill);
        }
        public void OnBuySkill(MySkills _skill)
        {
            if (m_SkillTree.SkillPoints < skillCost) return;

            Debug.Log("click");
            SkillBuyEvent evt = Events.SkillBuyEvent;
            evt.Skill = _skill;
            evt.SkillPrice = skillCost;
            EventManager.Broadcast(evt);

            skillButton.interactable = false;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowExplanation(true);
        }
        private void ShowExplanation(bool activate)
        {
            skillExplanationObject.SetActive(activate);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            ShowExplanation(false);
        }
    }
}

