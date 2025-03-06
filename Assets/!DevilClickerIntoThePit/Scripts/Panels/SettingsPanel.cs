using Game.Managers;
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

        public UnityAction DiggingAndDevilSpasesChanged;

        #endregion

        #region CORE

        [Header("Sound")]
        [SerializeField] private Button soundButton;
        [SerializeField] private Image soundImage;

        [SerializeField] private string pathToEnableSoundSprite;
        [SerializeField] private string pathToDisableSoundSprite;

        private bool _isSoundEnable;

        [Header("Digging and Devils")]
        [SerializeField] private Button switchDiggingAndDevilButton;
        [SerializeField] private Image switchDiggingAndDevilImage;

        [SerializeField] private string pathToDiggingSpaseSprite;
        [SerializeField] private string pathToDevilSpaseSprite;

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
                soundButton.onClick.AddListener(ToggleSound);
                switchDiggingAndDevilButton.onClick.AddListener(ToggleDiggingAndDevilSpases);
            }
            else
            {
                soundButton.onClick.RemoveListener(ToggleSound);
                switchDiggingAndDevilButton.onClick.RemoveListener(ToggleDiggingAndDevilSpases);
            }
        }

        #endregion

        #region CALLBACKS

        private void ToggleSound()
        {
            _isSoundEnable = !_isSoundEnable;
            Sprite sprite = Resources.Load<Sprite>(_isSoundEnable ? pathToEnableSoundSprite : pathToDisableSoundSprite);

            soundImage.sprite = sprite;
            Resources.UnloadUnusedAssets();

            if (_isSoundEnable)
            {
                EnableSound?.Invoke();
            }
            else
            {
                DisableSound?.Invoke();
            }
        }

        private void ToggleDiggingAndDevilSpases()
        {
            bool isDiggingSpaseActive = enemyManager.GetAndChangeIsDiggingSpaseActive();
            Sprite sprite = Resources.Load<Sprite>(isDiggingSpaseActive ? pathToDiggingSpaseSprite : pathToDevilSpaseSprite);

            switchDiggingAndDevilImage.sprite = sprite;
            Resources.UnloadUnusedAssets();

            DiggingAndDevilSpasesChanged?.Invoke();
        }

        #endregion
    }
}