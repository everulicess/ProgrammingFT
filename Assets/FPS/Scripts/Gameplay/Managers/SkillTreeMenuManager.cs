using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.Game;


namespace Unity.FPS.Gameplay
{ 
    public class SkillTreeMenuManager : MonoBehaviour
    {
        public int SkillPoints = 0;
        
        private void Start()
        {
            EventManager.AddListener<LevelUpEvent>(OnLevelUp);
            EventManager.AddListener<SkillBuyEvent>(OnSkillBuy);
        }

        private void OnLevelUp(LevelUpEvent _event)
        {
            SkillPoints += 2;
        }
        private void OnSkillBuy(SkillBuyEvent _event)
        {
            SkillPoints -= _event.SkillPrice;
        }
    }
}
