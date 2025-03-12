using UnityEngine;
using Game.Classes.Player;
using Game.Classes;
using YG;
using System;
using UnityEngine.SceneManagement;

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

        public Player Player { get; private set; }

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                InitializeValues();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += InitializeValues;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= InitializeValues;
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            if (YandexGame.SDKEnabled == true)
            {
                Player = (Player)YandexGame.savesData;
            }
        }

        #endregion

        #region CORE LOGIC

        public void CheckHealthPit()
        {
            if (Player.DevilPower >= EnemyManager.Instance.HealthPit)
            {
                EnemyManager.Instance.SetIsDiggingSpaseActive(false);
                PitClosed?.Invoke();

                EnemyManager.Instance.EndStoryPanelResource.LoadAndInstantiateResource();
            }
        }

        public void CheckHealthPitAtTheStart()
        {
            if (Player.DevilPower >= EnemyManager.Instance.HealthPit)
            {
                PitClosed?.Invoke();
            }
        }

        #endregion

        #region CALLBACKS

        public void GoToStartSceneOrShowStartStory()
        {
            if (Player.NumberOfExorcisedDevils == 0) // if just starting
            {
                startStoryPanelResource.LoadAndInstantiateResource();
            }
            else
            {
                if (YandexGame.SDKEnabled == true)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        #endregion
    }
}