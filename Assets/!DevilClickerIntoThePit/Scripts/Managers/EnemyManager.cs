using System;
using Game.Classes;
using Game.Panels.Characteristics;
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

        public EnemyInstance _selectableEnemy { get; private set; }

        [Header("UI")]
        [SerializeField] private Text healthText;
        [SerializeField] private Image enemyImage;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            _selectableEnemy = enemies[playerManager.Player.LevelOfDevil - 1].CreateInstance();
            _selectableEnemy.MultiplyHealth(playerManager.Player.NumberOfExorcisedDevils == 0 ? 
            1 : 
            playerManager.Player.NumberOfExorcisedDevils);
        }

        private void InitializeUI()
        {
            enemyImage.sprite = _selectableEnemy.DevilSprite;
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += InitializeValues;
                improvedDevilPanel.EnemyImproved += InitializeUI;
                improvedDevilPanel.EnemyImproved += UpdateHealthText;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= InitializeValues;
                improvedDevilPanel.EnemyImproved -= InitializeUI;
                improvedDevilPanel.EnemyImproved -= UpdateHealthText;
            }
        }

        #endregion

        #region UI

        public void TakeDamage()
        {
            AddDamage(playerManager.Player.Damage);

            if (_selectableEnemy.Health <= 0)
            {
                playerManager.Player.AddMoney(_selectableEnemy.Reward);
                playerManager.Player.AddExorcisedDevil();

                InitializeValues();

                DevilBanished.Invoke();
            }
            UpdateHealthText();
        }

        #endregion

        #region UI

        private void UpdateHealthText() => healthText.text = $"{_selectableEnemy.Health}";

        #endregion

        #region GET

        public ushort GetPriceNextDevil()
        {
            int index = playerManager.Player.LevelOfDevil;

            if (index < enemies.Length)
            {
                EnemyInstance nextEnemy = enemies[index].CreateInstance();

                return nextEnemy.Price;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region ADD

        private void AddDamage(int damage) => _selectableEnemy.ReduceHealth(damage);

        #endregion
    }
}