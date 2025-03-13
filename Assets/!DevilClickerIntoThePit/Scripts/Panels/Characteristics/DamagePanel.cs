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

        // Managers
        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;

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
            UpdateDamageText(_playerManager.Player.Damage);
            UpdateAutoDamageText(_playerManager.Player.AutoDamage);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                shopPanel.ManBought += InitializeUI;
            }
            else
            {
                shopPanel.ManBought -= InitializeUI;
            }
        }

        #endregion

        #region UI

        private void UpdateDamageText(int damage) => damageText.text = $"{damage}/ клик";
        private void UpdateAutoDamageText(int autoDamage) => autoDamageText.text = $"{autoDamage}/ в секунду";

        #endregion
    }
}