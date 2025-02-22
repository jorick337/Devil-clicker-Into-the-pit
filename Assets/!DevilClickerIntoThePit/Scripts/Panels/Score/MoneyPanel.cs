using Game.Managers;
using Game.Panels.Characteristics;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Score
{
    public class MoneyPanel : MonoBehaviour
    {
        #region CORE

        [Header("UI")]
        [SerializeField] private Text moneyText;

        [Header("Panels")]
        [SerializeField] private ShopPanel shopPanel;
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeUI()
        {
            UpdateMoneyText(playerManager.Player.Money);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                enemyManager.DevilBanished += InitializeUI;
                shopPanel.manBought += InitializeUI;
                improvedDevilPanel.EnemyImproved += InitializeUI;
            }
            else
            {
                enemyManager.DevilBanished -= InitializeUI;
                shopPanel.manBought -= InitializeUI;
                improvedDevilPanel.EnemyImproved -= InitializeUI;
            }
        }

        #endregion

        #region UI

        private void UpdateMoneyText(int money) => moneyText.text = $"{money} $";

        #endregion
    }
}