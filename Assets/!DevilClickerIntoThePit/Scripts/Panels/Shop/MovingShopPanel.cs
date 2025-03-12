using System;
using Game.Managers;
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

        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void OnEnable()
        {
            switchSwordsAndMenButton.onClick.AddListener(SwitchSwordsAndMen);
            backMoveButton.onClick.AddListener(MoveBack);
            nextMoveButton.onClick.AddListener(MoveForward);

            settingsPanel.DiggingAndDevilSpasesChanged += SwitchActiveSwitchButton;

            _playerManager.PitClosed += UndoChangesByDiggingSpase;
        }

        private void OnDisable()
        {
            switchSwordsAndMenButton.onClick.RemoveListener(SwitchSwordsAndMen);
            backMoveButton.onClick.RemoveListener(MoveBack);
            nextMoveButton.onClick.RemoveListener(MoveForward);

            settingsPanel.DiggingAndDevilSpasesChanged -= SwitchActiveSwitchButton;

            _playerManager.PitClosed -= UndoChangesByDiggingSpase;
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

        private void UndoChangesByDiggingSpase()
        {
            switchSwordsAndMenButton.gameObject.SetActive(true);
            settingsPanel.DiggingAndDevilSpasesChanged -= SwitchActiveSwitchButton;
        }

        private void SwitchSwordsAndMen() => ItemsSwitched?.Invoke();

        #endregion
    }
}