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
            UpdateMoneyText();
        }

        private void OnEnable()
        {
            enemyManager.DevilBanished += UpdateMoneyText;
            shopPanel.manBought += UpdateMoneyText;
            improvedDevilPanel.EnemyImproved += UpdateMoneyText;
            improvedDevilPanel.PitImproved += UpdateMoneyText;
        }

        private void OnDisable()
        {
            enemyManager.DevilBanished -= UpdateMoneyText;
            shopPanel.manBought -= UpdateMoneyText;
            improvedDevilPanel.EnemyImproved -= UpdateMoneyText;
            improvedDevilPanel.PitImproved -= UpdateMoneyText;
        }

        #endregion

        #region UI

        private void UpdateMoneyText() => moneyText.text = $"{playerManager.Player.Money} $";

        #endregion
    }
}