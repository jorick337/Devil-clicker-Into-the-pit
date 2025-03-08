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
        [SerializeField] private AchievementUI[] achievements;

        [Header("UI")]
        [SerializeField] private Button closePanelButton;

        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeManagers();
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeManagers()
        {
            _playerManager = PlayerManager.Instance;
            _enemyManager = EnemyManager.Instance;
        }

        private void InitializeUI()
        {
            UpdateAchievements(_playerManager.Player.NumberOfExorcisedDevils);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                _enemyManager.DevilBanished += InitializeUI;
                closePanelButton.onClick.AddListener(DestroyAndUnloadGameObject);
            }
            else
            {
                _enemyManager.DevilBanished -= InitializeUI;
                closePanelButton.onClick.RemoveListener(DestroyAndUnloadGameObject);
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

        #region CALLBACKS

        private void DestroyAndUnloadGameObject()
        {
            Destroy(gameObject);
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }

        #endregion
    }
}