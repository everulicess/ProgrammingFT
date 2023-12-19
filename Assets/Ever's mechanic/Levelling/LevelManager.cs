using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace EverMechanic
{
    
    public class LevelManager : MonoBehaviour
    {
        
        
        void Start()
        {
            EventManager.AddListener<LevelUpEvent>(LevelUp);
            EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
        }

        void Update()
        {

        }
        /// <summary>
        /// create Event in Events
        /// How to fire the evet = EventManager.Broadcast(new LevelUpEvent());
        /// If I want an object to fire an event have to add listener = EventManager.AddListener<LevelUpEvent>(LevelUp);
        /// Don't forget to remove the listener = EventManager.RemoveListener<LevelUpEvent>(LevelUp);
        /// </summary>
        /// <param name="_event"></param>
        void LevelUp(LevelUpEvent _event)
        {
            Debug.Log(" NEW LEVEL HAS REACHED");
        }

        void OnEnemyKilled(EnemyKillEvent _event)
        {
            Debug.Log("Gets Experience");

        }
    }
    
}

