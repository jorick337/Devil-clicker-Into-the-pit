using UnityEngine;
using Game.Classes;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region CORE

        public Player Player { get; private set; }

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
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