using System;
using Game.Classes;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Characteristics
{
    public class ImprovedDevilPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const string TITLE_IMPROVE_DEVIL = "Улучшить черта";
        private const string TITLE_IMPROVE_PIT = "Оптимизировать";

        private const string MAX_UPDATE = "МАКС";

        #endregion

        #region EVENTS

        public Action EnemyImproved;
        public Action PitImproved;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private ImprovingSettings improvingSettings;

        private ushort _nextPriceDevil;
        private int _nextPricePit;

        [Header("UI")]
        [SerializeField] private Button improveButton;
        [SerializeField] private Text titleText;
        [SerializeField] private Text priceText;

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

            InitializeValues();
            UpdatePriceAndTitleTexts();
        }

        private void OnEnable()
        {
            PitImproved += _playerManager.CheckHealthPit;
            improveButton.onClick.AddListener(ToggleImprove);

            settingsPanel.DiggingAndDevilSpasesChanged += UpdatePriceAndTitleTexts;

            _playerManager.PitClosed += UndoChangesByDiggingSpase;
        }

        private void OnDisable()
        {
            PitImproved += _playerManager.CheckHealthPit;
            improveButton.onClick.RemoveListener(ToggleImprove);

            settingsPanel.DiggingAndDevilSpasesChanged -= UpdatePriceAndTitleTexts;

            _playerManager.PitClosed -= UndoChangesByDiggingSpase;
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            _nextPriceDevil = enemyManager.GetPriceNextDevil();
            _nextPricePit = improvingSettings.GetPrice();
        }

        #endregion

        #region UI

        private void UpdatePriceAndTitleTexts()
        {
            string textOfTitle;
            string textOfPrice;

            if (enemyManager.IsDiggingSpaseActive)
            {
                textOfTitle = TITLE_IMPROVE_PIT;
                textOfPrice = IsPitLevelEnd() ? MAX_UPDATE : $"{_nextPricePit} $";
            }
            else
            {
                textOfTitle = TITLE_IMPROVE_DEVIL;
                textOfPrice = IsDevilsLevelEnd() ? MAX_UPDATE : $"{_nextPriceDevil} $";
            }

            titleText.text = textOfTitle;
            priceText.text = textOfPrice;
        }

        #endregion

        #region CALLBACKS

        private void ToggleImprove()
        {
            if (enemyManager.IsDiggingSpaseActive)
            {
                if (_playerManager.Player.Money >= _nextPricePit && !IsPitLevelEnd())
                {
                    _playerManager.Player.BuyLevelOfPit(_nextPricePit);

                    enemyManager.SetPercentageOfPitHealth(improvingSettings.GetPercentage());
                    PitImproved?.Invoke();

                    _nextPricePit = improvingSettings.GetPrice();
                    UpdatePriceAndTitleTexts();
                }
            }
            else
            {
                if (_playerManager.Player.Money >= _nextPriceDevil && !IsDevilsLevelEnd())
                {
                    _playerManager.Player.BuyLevelOfDevil(_nextPriceDevil);

                    _nextPriceDevil = enemyManager.GetPriceNextDevil();
                    UpdatePriceAndTitleTexts();

                    EnemyImproved?.Invoke();
                }
            }
        }

        private void UndoChangesByDiggingSpase()
        {
            UpdatePriceAndTitleTexts();
            settingsPanel.DiggingAndDevilSpasesChanged -= UpdatePriceAndTitleTexts;
        }

        #endregion

        #region BOOL

        private bool IsDevilsLevelEnd() => _nextPriceDevil == 0;
        private bool IsPitLevelEnd() => _nextPricePit == 0;

        #endregion
    }
}