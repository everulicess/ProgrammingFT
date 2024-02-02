using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.FPS.Game;


namespace Unity.FPS.UI
{ 
    public class SkillTreeMenuManager : MonoBehaviour
    {
        public int SkillPoints = 0;
        int skillPointsToAdd = 5;

        [SerializeField] TextMeshProUGUI skillPointsTextHolder;
        private void Start()
        {
            EventManager.AddListener<LevelUpEvent>(OnLevelUp);
            EventManager.AddListener<SkillBuyEvent>(OnSkillBuy);
            skillPointsTextHolder.text = $"Skill Points: {SkillPoints}";
        }
        private void OnLevelUp(LevelUpEvent _event)
        {
            SkillPoints += skillPointsToAdd;
            skillPointsTextHolder.text = $"Skill Points: {SkillPoints}";
        }
        private void OnSkillBuy(SkillBuyEvent _event)
        {
            SkillPoints -= _event.SkillPrice;
            skillPointsTextHolder.text = $"Skill Points: {SkillPoints}";
        }
    }
}
