using Game.Panels;
using UnityEngine;

namespace Game.Managers
{
    public class SoundManager : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private AudioSource devilClickAudioSource;
        [SerializeField] private AudioSource devilDeathAudioSource;

        [Header("Panels")]
        [SerializeField] private SettingsPanel settingsPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void OnEnable()
        {
            _playerManager.PitClosed += DisableAllSound;

            enemyManager.HealthChanged += devilClickAudioSource.Play;
            enemyManager.DevilBanished += devilDeathAudioSource.Play;

            settingsPanel.DisableSound += DisableAllSound;
            settingsPanel.EnableSound += EnableAllSound;
        }

        private void OnDisable()
        {
            _playerManager.PitClosed -= DisableAllSound;

            enemyManager.HealthChanged -= devilClickAudioSource.Play;
            enemyManager.DevilBanished -= devilDeathAudioSource.Play;

            settingsPanel.DisableSound -= DisableAllSound;
            settingsPanel.EnableSound -= EnableAllSound;
        }

        #endregion

        #region CALLBACKS

        private void DisableAllSound()
        {
            devilClickAudioSource.volume = 0f;
            devilDeathAudioSource.volume = 0f;
        }

        private void EnableAllSound()
        {
            devilClickAudioSource.volume = 1f;
            devilDeathAudioSource.volume = 1f;
        }

        #endregion
    }
}