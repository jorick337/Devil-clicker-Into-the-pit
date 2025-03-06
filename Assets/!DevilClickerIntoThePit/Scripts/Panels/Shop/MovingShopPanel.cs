using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Shop
{
    public class MovingShopPanel : MonoBehaviour
    {
        #region EVENTS

        public Action ItemsSwitched;
        public Action NextItemsMoved;
        public Action PastItemsMoved;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Button switchSwordsAndMenButton;
        [SerializeField] private Button backMoveButton;
        [SerializeField] private Button nextMoveButton;

        [Header("Panels")]
        [SerializeField] private SettingsPanel settingsPanel;

        #endregion

        #region MONO

        private void OnEnable()
        {
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                switchSwordsAndMenButton.onClick.AddListener(SwitchSwordsAndMen);
                backMoveButton.onClick.AddListener(MoveBack);
                nextMoveButton.onClick.AddListener(MoveForward);

                settingsPanel.DiggingAndDevilSpasesChanged += SwitchActiveSwitchButton;
            }
            else
            {
                switchSwordsAndMenButton.onClick.RemoveListener(SwitchSwordsAndMen);
                backMoveButton.onClick.RemoveListener(MoveBack);
                nextMoveButton.onClick.RemoveListener(MoveForward);

                settingsPanel.DiggingAndDevilSpasesChanged -= SwitchActiveSwitchButton;
            }
        }

        #endregion

        #region UI

        private void SwitchActiveMovingButtons()
        {
            nextMoveButton.gameObject.SetActive(backMoveButton.gameObject.activeSelf);
            backMoveButton.gameObject.SetActive(!backMoveButton.gameObject.activeSelf);
        }

        private void SwitchActiveSwitchButton()
        {
            switchSwordsAndMenButton.gameObject.SetActive(!switchSwordsAndMenButton.gameObject.activeSelf);
        }

        #endregion

        #region CALLBACKS

        private void SwitchSwordsAndMen() => ItemsSwitched?.Invoke();

        private void MoveBack()
        {
            SwitchActiveMovingButtons();
            PastItemsMoved?.Invoke();
        }

        private void MoveForward()
        {
            SwitchActiveMovingButtons();
            NextItemsMoved?.Invoke();
        }

        #endregion
    }
}