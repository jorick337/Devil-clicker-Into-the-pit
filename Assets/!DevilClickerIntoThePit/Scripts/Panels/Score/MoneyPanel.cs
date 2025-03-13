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
        [SerializeField] private EnemyManager enemyManager;

        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;

            UpdateMoneyText();
        }

        private void OnEnable()
        {
            enemyManager.DevilBanished += UpdateMoneyText;
            shopPanel.ManBought += UpdateMoneyText;
            improvedDevilPanel.EnemyImproved += UpdateMoneyText;
            improvedDevilPanel.PitImproved += UpdateMoneyText;
        }

        private void OnDisable()
        {
            enemyManager.DevilBanished -= UpdateMoneyText;
            shopPanel.ManBought -= UpdateMoneyText;
            improvedDevilPanel.EnemyImproved -= UpdateMoneyText;
            improvedDevilPanel.PitImproved -= UpdateMoneyText;
        }

        private void FixedUpdate()
        {
            UpdateMoneyText();
        }

        #endregion

        #region UI

        private void UpdateMoneyText() => moneyText.text = $"{_playerManager.Player.Money} $";

        #endregion
    }
}