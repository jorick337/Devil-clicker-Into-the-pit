using UnityEngine;

namespace Game.Managers
{
    public class SoundManager : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private AudioSource devilClickAudioSource;
        [SerializeField] private AudioSource devilDeathAudioSource;

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
            }
            else
            {
                enemyManager.HealthChanged -= devilClickAudioSource.Play;
                enemyManager.DevilBanished -= devilDeathAudioSource.Play;
            }
        }

        #endregion
    }
}