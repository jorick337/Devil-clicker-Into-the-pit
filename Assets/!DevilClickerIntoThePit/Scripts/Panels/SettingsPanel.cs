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
        [SerializeField] private Button soundButton;
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
                soundButton.onClick.AddListener(EnableAllSound);
            }
            else
            {
                soundButton.onClick.RemoveListener(EnableAllSound);
            }
        }

        #endregion

        #region CALLBACKS

        private void EnableAllSound()
        {
            soundImage.sprite = enableSprite;
            soundImage.raycastTarget = true;

            soundButton.onClick.RemoveListener(EnableAllSound);
            soundButton.onClick.AddListener(DisableAllSound);

            EnableSound.Invoke();
        }

        private void DisableAllSound()
        {
            soundImage.sprite = disableSprite;
            soundImage.raycastTarget = false;

            soundButton.onClick.RemoveListener(DisableAllSound);
            soundButton.onClick.AddListener(EnableAllSound);

            DisableSound.Invoke();
        }

        #endregion
    }
}