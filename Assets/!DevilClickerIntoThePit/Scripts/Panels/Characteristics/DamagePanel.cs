using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Characteristics
{
    public class DamagePanel : MonoBehaviour
    {
        #region CORE

        [Header("UI")]
        [SerializeField] private Text damageText;
        [SerializeField] private Text autoDamageText;

        [Header("Panels")]
        [SerializeField] private ShopPanel shopPanel;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

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
            UpdateDamageText(playerManager.Player.Damage);
            UpdateAutoDamageText(playerManager.Player.AutoDamage);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                shopPanel.manBought += InitializeUI;
            }
            else
            {
                shopPanel.manBought -= InitializeUI;
            }
        }

        #endregion

        #region UI

        private void UpdateDamageText(int damage) => damageText.text = $"{damage}/ клик";
        private void UpdateAutoDamageText(int autoDamage) => autoDamageText.text = $"{autoDamage}/ в секунду";

        #endregion
    }
}