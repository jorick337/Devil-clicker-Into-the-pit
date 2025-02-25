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
 
        #endregion

        #region MONO

        private void Awake()
        {
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                enemyManager.HealthChanged += devilClickAudioSource.Play;
                enemyManager.DevilBanished += devilDeathAudioSource.Play;

                settingsPanel.DisableSound += DisableAllSound;
                settingsPanel.EnableSound += EnableAllSound;
            }
            else
            {
                enemyManager.HealthChanged -= devilClickAudioSource.Play;
                enemyManager.DevilBanished -= devilDeathAudioSource.Play;

                settingsPanel.DisableSound -= DisableAllSound;
                settingsPanel.EnableSound -= EnableAllSound;
            }
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