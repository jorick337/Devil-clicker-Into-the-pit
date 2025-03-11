using UnityEngine;
using Game.Classes.Player;
using Game.Classes;
using YG;
using System;
using Game.Panels;
using Game.Panels.Enemy;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region #EVENTS

        public Action PitClosed;

        #endregion

        #region CORE

        public static PlayerManager Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private Resource startStoryPanelResource;
        [SerializeField] private Resource endStoryPanelResource;

        public Player Player { get; private set; }

        [Header("Panels")]
        [SerializeField] private EnemyPanel enemyPanel;
        [SerializeField] private ShopPanel shopPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                InitializeValues();
                InitializeStartStoryPanel();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (Player.DevilPower >= enemyManager.HealthPit)
            {
                PitClosed?.Invoke();
            }
        }

        private void OnEnable()
        {
            enemyPanel.HealthPitChanged += CheckHealthPit;
            shopPanel.manBought += CheckHealthPit;
        }

        private void OnDisable()
        {
            enemyPanel.HealthPitChanged -= CheckHealthPit;
            shopPanel.manBought -= CheckHealthPit;

            YandexGame.savesData += Player;
            YandexGame.SaveProgress();
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            Player = (Player)YandexGame.savesData;
        }

        private void InitializeStartStoryPanel()
        {
            if (Player.NumberOfExorcisedDevils == 0) // if just starting
            {
                startStoryPanelResource.LoadAndInstantiateResource();
            }
        }

        #endregion

        #region CORE LOGIC

        private void CheckHealthPit()
        {
            if (Player.DevilPower >= enemyManager.HealthPit)
            {
                enemyManager.SetIsDiggingSpaseActive(false);
                PitClosed?.Invoke();

                endStoryPanelResource.LoadAndInstantiateResource();
            }
        }

        #endregion
    }
}