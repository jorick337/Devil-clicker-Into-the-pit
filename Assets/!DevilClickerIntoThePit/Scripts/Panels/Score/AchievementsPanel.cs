using Game.Classes;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Score
{
    public class AchievementsPanel : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private Achievement[] achievements;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeUI()
        {
            UpdateAchievements(playerManager.Player.NumberOfExorcisedDevils);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                enemyManager.DevilBanished += InitializeUI;
            }
            else
            {
                enemyManager.DevilBanished -= InitializeUI;
            }
        }

        #endregion

        #region UI

        private void UpdateAchievements(ushort numberOfExorcisedDevils)
        {
            foreach (var achievement in achievements)
            {
                achievement.UpdateProgress(numberOfExorcisedDevils);
            }
        }

        #endregion
    }
}