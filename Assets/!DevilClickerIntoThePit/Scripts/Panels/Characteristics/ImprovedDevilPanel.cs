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
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            UpdatePriceAndTitleTexts();
        }

        private void OnEnable()
        {
            improveButton.onClick.AddListener(ToggleImprove);
            settingsPanel.DiggingAndDevilSpasesChanged += UpdatePriceAndTitleTexts;
        }

        private void OnDisable()
        {
            improveButton.onClick.RemoveListener(ToggleImprove);
            settingsPanel.DiggingAndDevilSpasesChanged -= UpdatePriceAndTitleTexts;
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
                textOfPrice = IsPitLevelEnd() ? MAX_UPDATE : $"{_nextPricePit - 0.01f} $";
            }
            else
            {
                textOfTitle = TITLE_IMPROVE_DEVIL;
                textOfPrice = IsDevilsLevelEnd() ? MAX_UPDATE : $"{_nextPriceDevil - 0.01f} $";
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
                if (playerManager.Player.Money >= _nextPricePit && !IsPitLevelEnd())
                {
                    playerManager.Player.ReduceMoney(_nextPricePit);

                    enemyManager.SetPercentageOfPitHealth(improvingSettings.GetPercentage());
                    PitImproved?.Invoke();

                    playerManager.Player.AddMaxLevelOfPit();
                    _nextPricePit = improvingSettings.GetPrice();
                    UpdatePriceAndTitleTexts();
                }
            }
            else
            {
                if (playerManager.Player.Money >= _nextPriceDevil && !IsDevilsLevelEnd())
                {
                    playerManager.Player.ReduceMoney(_nextPriceDevil);
                    playerManager.Player.AddMaxLevelOfDevil();

                    _nextPriceDevil = enemyManager.GetPriceNextDevil();
                    UpdatePriceAndTitleTexts();

                    EnemyImproved?.Invoke();
                }
            }
        }

        #endregion

        #region BOOL

        private bool IsDevilsLevelEnd() => _nextPriceDevil == 0;
        private bool IsPitLevelEnd() => _nextPricePit == 0;

        #endregion
    }
}