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
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _isSoundEnable = true;
        }

        private void OnEnable()
        {
            soundButton.onClick.AddListener(ToggleSound);
            switchDiggingAndDevilButton.onClick.AddListener(ToggleDiggingAndDevilSpases);

            playerManager.PitClosed += DestroySwitchDiggingAndDevilSpasesButton;
        }

        private void OnDisable()
        {
            soundButton.onClick.RemoveListener(ToggleSound);
            switchDiggingAndDevilButton.onClick.RemoveListener(ToggleDiggingAndDevilSpases);

            playerManager.PitClosed -= DestroySwitchDiggingAndDevilSpasesButton;
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

        private void DestroySwitchDiggingAndDevilSpasesButton()
        {
            switchDiggingAndDevilButton.onClick.RemoveListener(ToggleDiggingAndDevilSpases);
            Destroy(switchDiggingAndDevilButton.gameObject);

            Resources.UnloadUnusedAssets();
        }

        #endregion
    }
}