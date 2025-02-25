using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Panels
{
    public class SettingsPanel : MonoBehaviour
    {
        #region EVENTS

        public UnityAction DisableSound;
        public UnityAction EnableSound;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Sprite enableSprite;
        [SerializeField] private Sprite disableSprite;

        [Header("UI")]
        [SerializeField] private Button enableSoundButton;
        [SerializeField] private Button disableSoundButton;

        [SerializeField] private Image soundImage;

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
                enableSoundButton.onClick.AddListener(EnableAllSound);
                disableSoundButton.onClick.AddListener(DisableAllSound);
            }
            else
            {
                enableSoundButton.onClick.RemoveListener(EnableAllSound);
                disableSoundButton.onClick.RemoveListener(DisableAllSound);
            }
        }

        #endregion

        #region CALLBACKS

        private void EnableAllSound()
        {
            soundImage.sprite = enableSprite;
            soundImage.raycastTarget = true;

            EnableSound.Invoke();
        }

        private void DisableAllSound()
        {
            soundImage.sprite = disableSprite;
            soundImage.raycastTarget = false;

            DisableSound.Invoke();
        }

        #endregion
    }
}