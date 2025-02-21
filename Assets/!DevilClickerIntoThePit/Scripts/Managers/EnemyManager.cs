using System;
using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region EVENTS

        public event Action DevilBanished;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Enemy[] enemies;

        private EnemyInstance _selectableEnemy;

        [Header("UI")]
        [SerializeField] private Text healthText;
        [SerializeField] private Image enemyImage;

        [Header("Managers")]
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
            _selectableEnemy = enemies[playerManager.Player.LevelOfDevil - 1].CreateInstance();
        }

        private void InitializeUI()
        {
            enemyImage = _selectableEnemy.DevilImage;
        }

        #endregion

        #region UI

        public void TakeDamage()
        {
            AddDamage(playerManager.Player.Damage);

            if (_selectableEnemy.Health <= 0)
            {
                playerManager.Player.AddMoney(_selectableEnemy.Money);

                InitializeValues();
                DevilBanished.Invoke();
            }
            SetText(_selectableEnemy.Health);
        }

        #endregion

        #region SET

        private void SetText(int health) => healthText.text = $"{health}";

        #endregion

        #region ADD

        private void AddDamage(int damage)
        {
            _selectableEnemy.Health -= damage;
        }

        #endregion
    }
}