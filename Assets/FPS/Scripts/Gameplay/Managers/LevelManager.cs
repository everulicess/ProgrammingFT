using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Game;


namespace Unity.FPS.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        public Image LevelIndicator;
        //public TMPro.TextMeshProUGUI LevelNumberText;
        
        int levelNumber;
        float maxExperience = 20;
        float currentExperience = 0;
        void Start()
        {
            //    EventManager.AddListener<LevelUpEvent>(LevelUp);
            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
            
        }
        private void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
        }
        void Update()
        {
            LevelIndicator.fillAmount = currentExperience/maxExperience;
            //LevelNumberText.text = levelNumber.ToString(); 
            if (currentExperience == maxExperience)
            {
                Debug.Log("NewLevelReached");
            }
        }


        /// <summary>
        /// create Event in Events
        /// How to fire the evet = EventManager.Broadcast(new LevelUpEvent());
        /// If I want an object to fire an event have to add listener = EventManager.AddListener<LevelUpEvent>(LevelUp);
        /// Don't forget to remove the listener = EventManager.RemoveListener<LevelUpEvent>(LevelUp);
        /// </summary>
        /// <param name="_event"></param>
        //void LevelUp(LevelUpEvent _event)
        //{
        //    _event.DebugSomeething("NEW LEVEL HAS BEEN REACHED");
        //}

        void OnEnemyKilled(EnemyKillEvent _event)
        {
            
            currentExperience += 15;
            
            if (currentExperience >= maxExperience)
            {
                levelNumber++;
                currentExperience -= maxExperience;
                maxExperience *= 1.50f; ;
                Debug.Log("NewLevelReached");
                EventManager.Broadcast(new LevelUpEvent());
            }
            //Debug.Log($"Current fill amount: {LevelIndicator.fillAmount}");
            //Debug.Log($"Current exp: {currentExperience}|||| current Max Exp: {maxExperience}");
            //Debug.Log("Gets Experience");

        }
    }
    
}

