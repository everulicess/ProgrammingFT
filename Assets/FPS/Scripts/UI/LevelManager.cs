using UnityEngine;
using UnityEngine.UI;
using TMPro;



namespace Unity.FPS.Game
{
    public class LevelManager : MonoBehaviour
    {
        public Image LevelIndicator;
        [SerializeField] TextMeshProUGUI LevelNumberText;
        
        int levelNumber;
        float maxExperience = 20f;
        float currentExperience = 0f;
        float experience = 15f;
        void Start()
        {
            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
        }
        private void OnDestroy()
        {
            EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
        }
        void Update()
        {
            UpdateUI();
            if (currentExperience == maxExperience)
            {
                Debug.Log("NewLevelReached");
            }
        }
        private void UpdateUI()
        {
            LevelIndicator.fillAmount = currentExperience / maxExperience;
            LevelNumberText.text = levelNumber.ToString();
        }

        void OnEnemyKilled(EnemyKillEvent _event)
        {
            currentExperience += experience;
            
            if (currentExperience >= maxExperience)
            {
                levelNumber++;
                currentExperience -= maxExperience;
                maxExperience *= 1.50f; ;
                Debug.Log("NewLevelReached");
                LevelUpEvent evt = new LevelUpEvent();
                EventManager.Broadcast(evt);
            }
        }
    }
}

