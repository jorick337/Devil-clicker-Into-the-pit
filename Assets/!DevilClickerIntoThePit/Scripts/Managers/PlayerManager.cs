using UnityEngine;
using Game.Classes.Player;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region CORE

        public static PlayerManager Instance { get; private set; }

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

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            Player = new();
        }

        #endregion
    }
}