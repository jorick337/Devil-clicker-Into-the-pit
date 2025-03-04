using System;
using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Characteristics
{
    public class ImprovedDevilPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const string MAX_UPDATE = "MAX";

        #endregion

        #region EVENTS

        public Action EnemyImproved;

        #endregion

        #region CORE

        private ushort _nextPriceDevil;

        [Header("UI")]
        [SerializeField] private Text priceText;
        
        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            InitializeUI();
        }
        
        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            _nextPriceDevil = enemyManager.GetPriceNextDevil();
        }

        private void InitializeUI()
        {
            UpdatePriceText();
        }

        #endregion

        #region UI

        public void ImproveDevil()
        {
            if (playerManager.Player.Money >= _nextPriceDevil && !IsDevilsLevelEnd())
            {
                playerManager.Player.ReduceMoney(_nextPriceDevil);
                playerManager.Player.AddMaxLevelOfDevil();

                InitializeValues();

                EnemyImproved.Invoke();
                Resources.UnloadUnusedAssets();
            }

            UpdatePriceText();
        }

        private void UpdatePriceText() 
        {
            if (IsDevilsLevelEnd())
            {
                priceText.text = MAX_UPDATE;
            }
            else
            {
                priceText.text = $"{_nextPriceDevil - 0.01f} $";
            }
        }

        #endregion

        #region BOOL

        private bool IsDevilsLevelEnd() => _nextPriceDevil == 0;

        #endregion
    }
}