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

        public UnityAction EnableDigging;
        public UnityAction EnableDevil;

        #endregion

        #region CORE

        [Header("Sound")]
        [SerializeField] private Button soundButton;
        [SerializeField] private Image soundImage;
        [SerializeField] private Sprite enableSoundSprite;
        [SerializeField] private Sprite disableSoundSprite;

        [Header("Digging and Devils")]
        [SerializeField] private Button switchDiggingAndDevilButton;
        [SerializeField] private Image switchDiggingAndDevilImage;
        [SerializeField] private Sprite enableDiggingSprite;
        [SerializeField] private Sprite enableDevilSprite;

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
                soundButton.onClick.AddListener(ToggleSound);
                switchDiggingAndDevilButton.onClick.AddListener(ToggleDiggingAndDevil);
            }
            else
            {
                soundButton.onClick.RemoveListener(ToggleSound);
                switchDiggingAndDevilButton.onClick.RemoveListener(ToggleDiggingAndDevil);
            }
        }

        #endregion

        #region CALLBACKS

        private void ToggleSound()
        {
            bool isSoundEnabled = soundImage.sprite == disableSoundSprite;

            soundImage.sprite = isSoundEnabled ? enableSoundSprite : disableSoundSprite;
            Resources.UnloadUnusedAssets();

            if (isSoundEnabled)
            {
                EnableSound?.Invoke();
            }
            else
            {
                DisableSound?.Invoke();
            }
        }

        private void ToggleDiggingAndDevil()
        {
            bool isDiggingEnabled = switchDiggingAndDevilImage.sprite == enableDiggingSprite;

            switchDiggingAndDevilImage.sprite = isDiggingEnabled ? enableDevilSprite : enableDiggingSprite;
            Resources.UnloadUnusedAssets();

            if (isDiggingEnabled)
            {
                EnableDevil?.Invoke();
            }
            else
            {
                EnableDigging?.Invoke();
            }
        }

        #endregion
    }
}