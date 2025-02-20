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
            
        }

        private void FixedUpdate()
        {
            
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            Player = new();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
            }
            else
            {

            }
        }

        #endregion

        #region ADD

        public void AddProgress()
        {
            
        }

        #endregion

        #region CALLBACKS

        private void AddProgressByClick() => AddProgress();

        #endregion
    }
}