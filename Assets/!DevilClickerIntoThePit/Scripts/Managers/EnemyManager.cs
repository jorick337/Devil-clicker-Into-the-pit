using System;
using Game.Classes;
using Game.Panels.Characteristics;
using UnityEngine;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region EVENTS

        public event Action DevilBanished;
        public event Action HealthChanged;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Enemy[] enemies;

        public EnemyInstance SelectableEnemy { get; private set; }

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            RegisterEvents(true);
        }

        private void Start()
        {
            InvokeRepeating("TakeAutoDamage", 1f, 1f);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            SelectableEnemy = enemies[playerManager.Player.LevelOfDevil - 1].CreateInstance(playerManager.Player.NumberOfExorcisedDevils);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += InitializeValues;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= InitializeValues;
            }
        }

        #endregion

        #region CORE LOGIC

        public void TakeDamage()
        {
            AddDamage(playerManager.Player.Damage);
            CheckEnemyHealth();
        }

        private void TakeAutoDamage()   // Используется в Start
        {
            if (playerManager.Player.AutoDamage > 0)
            {
                AddDamage(playerManager.Player.AutoDamage);
                CheckEnemyHealth();
            }
        }

        private void CheckEnemyHealth()
        {
            if (SelectableEnemy.Health <= 0)
            {
                playerManager.Player.AddMoney(SelectableEnemy.Reward);
                playerManager.Player.AddExorcisedDevil();

                InitializeValues();

                DevilBanished.Invoke();
            }

            HealthChanged.Invoke();
        }

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

        public float GetPercentageOfHealth()
        {
            EnemyInstance initialEnemy = enemies[playerManager.Player.LevelOfDevil - 1].CreateInstance(playerManager.Player.NumberOfExorcisedDevils);
            float percentage = Math.Abs((float)SelectableEnemy.Health / initialEnemy.Health - 1);

            return percentage == 1 ? 0 : percentage;
        }

        #endregion

        #region ADD

        private void AddDamage(int damage) => SelectableEnemy.ReduceHealth(damage);

        #endregion
    }
}