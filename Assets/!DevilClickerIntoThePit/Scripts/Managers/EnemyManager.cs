using System;
using Game.Classes;
using Game.Panels.Characteristics;
using UnityEngine;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region 

        private const ushort PIT_HEALTH = 50000;

        #endregion

        #region EVENTS

        public event Action DevilBanished;
        public event Action HealthChanged;
        public event Action DevilChanged;

        #endregion

        #region CORE

        public static EnemyManager Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private ImprovingSettings improvingSettings;
        [SerializeField] private Enemy[] enemies;

        public EnemyInstance SelectableEnemy { get; private set; }
        public byte SelectedIndexDevil { get; private set; }

        public bool IsDiggingSpaseActive { get; private set; }
        public ushort HealthPit { get; private set; }

        [Header("Resources")]
        [SerializeField] private Resource endStoryPanelResource;

        public Resource EndStoryPanelResource => endStoryPanelResource;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                _playerManager = PlayerManager.Instance;
                InitializeValues();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InvokeRepeating("TakeAutoDamage", 1f, 1f);
            _playerManager.CheckHealthPitAtTheStart();
        }

        private void OnEnable()
        {
            improvedDevilPanel.EnemyImproved += InitializeMaxSelectedIndexDevil;
            improvedDevilPanel.EnemyImproved += InitializeEnemy;
        }

        private void OnDisable()
        {
            improvedDevilPanel.EnemyImproved -= InitializeMaxSelectedIndexDevil;
            improvedDevilPanel.EnemyImproved -= InitializeEnemy;
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            InitializeMaxSelectedIndexDevil();
            InitializeEnemy();

            IsDiggingSpaseActive = false;
            InitializePit();
        }

        private void InitializeMaxSelectedIndexDevil()
        {
            SelectedIndexDevil = (byte)(_playerManager.Player.MaxLevelOfDevil - 1);
        }

        private void InitializeEnemy()
        {
            SelectableEnemy = enemies[SelectedIndexDevil].CreateInstance(_playerManager.Player.NumberOfExorcisedDevils);
        }

        private void InitializePit()
        {
            HealthPit = PIT_HEALTH;
            SetPercentageOfPitHealth(improvingSettings.GetPercentage());
        }

        #endregion

        #region CORE LOGIC

        public void TakeDamage()
        {
            SelectableEnemy.ReduceHealth(_playerManager.Player.Damage);
            CheckEnemyHealth();
        }

        private void TakeAutoDamage()   // Используется в Start
        {
            if (_playerManager.Player.AutoDamage > 0)
            {
                SelectableEnemy.ReduceHealth(_playerManager.Player.AutoDamage);
                CheckEnemyHealth();
            }
        }

        private void CheckEnemyHealth()
        {
            if (SelectableEnemy.Health <= 0)
            {
                _playerManager.Player.AddDeathDevil(SelectableEnemy.Reward, SelectedIndexDevil);

                InitializeEnemy();
                DevilBanished.Invoke();
            }

            HealthChanged.Invoke();
        }

        #endregion

        #region GET

        public ushort GetPriceNextDevil()
        {
            int index = _playerManager.Player.MaxLevelOfDevil;

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
            float percentage;

            if (!IsDiggingSpaseActive)
            {
                EnemyInstance initialEnemy = enemies[SelectedIndexDevil].CreateInstance(_playerManager.Player.NumberOfExorcisedDevils);
                percentage = Math.Abs((float)SelectableEnemy.Health / initialEnemy.Health - 1);
            }
            else
            {
                percentage = Math.Abs(_playerManager.Player.DevilPower / HealthPit - 1);
            }

            return percentage == 1 ? 0 : percentage;
        }

        public bool GetAndChangeIsDiggingSpaseActive() => IsDiggingSpaseActive = !IsDiggingSpaseActive; // каждый вызов сменяется
        public string GetHealth() => IsDiggingSpaseActive ? $"{_playerManager.Player.DevilPower}/{HealthPit}" : SelectableEnemy.Health.ToString();

        #endregion

        #region SET

        public void SetIsDiggingSpaseActive(bool active) => IsDiggingSpaseActive = active;
        public void SetPercentageOfPitHealth(float percentage) => HealthPit = (ushort)(PIT_HEALTH * percentage);
        public void SetSelectedIndexDevil(byte value) => SelectedIndexDevil = value;

        #endregion

        #region ADD

        public void AddSelectedIndexDevil()
        {
            SelectedIndexDevil += 1;
            InitializeEnemy();

            DevilChanged?.Invoke();
        }

        #endregion

        #region REDUCE

        public void ReduceSelectedIndexDevil()
        {
            SelectedIndexDevil -= 1;
            InitializeEnemy();

            DevilChanged?.Invoke();
        }

        #endregion
    }
}