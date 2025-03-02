using UnityEngine;
using Game.Classes;
using YG;

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

        private void OnDisable()
        {
            YandexGame.savesData.Player = Player;
            YandexGame.SaveProgress();
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            Player = YandexGame.savesData.Player;
        }

        #endregion
    }
}