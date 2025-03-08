using UnityEngine;
using Game.Classes.Player;
using Game.Classes;
using Game.Panels;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region CORE

        public static PlayerManager Instance { get; private set; }

        [Header("Core")]
        [SerializeField] private Resource storyPanelResource;

        public Player Player { get; private set; }

        private StartStoryPanel _storyPanel;

        #endregion

        #region MONO

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                InitializeValues();
                InitializeStoryPanel();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            Player = new();
        }

        private void InitializeStoryPanel()
        {
            if (Player.NumberOfExorcisedDevils == 0) // if just starting
            {
                _storyPanel = storyPanelResource.GetInstantiateGameObject<StartStoryPanel>();
            }
        }

        #endregion
    }
}