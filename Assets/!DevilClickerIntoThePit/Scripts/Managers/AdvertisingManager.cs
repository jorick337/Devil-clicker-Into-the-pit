using UnityEngine;
using YG;

namespace Game.Managers
{
    public class AdvertisingManager : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private byte chance;

        #endregion

        #region MONO

        private void Start()
        {
            InvokeRepeating("TryShowFullscreenAdWithChance", 1f, 61f);
        }

        #endregion

        #region CORE LOGIC

        public void TryShowFullscreenAdWithChance()
        {
            int random = Random.Range(0, 101);

            if (chance < random)
            {
                return;
            }

            YandexGame.FullscreenShow();
        }

        #endregion
    }
}