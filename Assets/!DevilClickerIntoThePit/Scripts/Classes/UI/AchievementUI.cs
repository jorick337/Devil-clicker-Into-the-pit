using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class AchievementUI : MonoBehaviour
    {
        #region CONSTANTS

        private const string CLOSING_PHRASE = "Достигнут";

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Text progressText;
        [SerializeField] private ushort maxProgress;

        #endregion

        #region UI

        public void UpdateProgress(ushort progress)
        {
            string text = progress >= maxProgress ?
                CLOSING_PHRASE : 
                $"{progress}/{maxProgress}";

            SetText(text);
        }

        #endregion

        #region SET

        private void SetText(string value) => progressText.text = value;

        #endregion
    }
}