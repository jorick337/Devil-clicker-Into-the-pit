using System;
using Game.Classes;
using Game.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Panels
{
    public class ShopPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const byte LENGHT_OF_TABLE = 3;
        private const byte LENGHT_OF_IDENTICAL_OBJECTS = 6;

        #endregion

        #region EVENTS

        public Action manBought;

        private UnityAction[] menBought;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Weapon[] weapons;
        [SerializeField] private ItemShopUI[] itemShopUI;

        private byte _focusedTableIndex;

        [Header("Moving")]
        [SerializeField] private Button switchSwordsAndMenButton;
        [SerializeField] private Button backMoveButton;
        [SerializeField] private Button nextMoveButton;

        private GameObject _backMoveGameObject => backMoveButton.gameObject;
        private GameObject _nextMoveGameObject => nextMoveButton.gameObject;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            menBought = new UnityAction[weapons.Length];
            for (byte i = 0; i < weapons.Length; i++)
            {
                byte index = i;

                menBought[i] = () => BuyMan(weapons[index].GetInstance());
            }

            _focusedTableIndex = 0;
        }

        private void InitializeUI()
        {
            for (int i = 0; i < LENGHT_OF_TABLE; i++)
            {
                itemShopUI[i].InitializeUI(weapons[_focusedTableIndex + i].GetInstance());
            }
            Resources.UnloadUnusedAssets();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                switchSwordsAndMenButton.onClick.AddListener(SwitchSwordsAndMen);
                backMoveButton.onClick.AddListener(MoveBack);
                nextMoveButton.onClick.AddListener(MoveForward);
            }
            else
            {
                switchSwordsAndMenButton.onClick.RemoveListener(SwitchSwordsAndMen);
                backMoveButton.onClick.RemoveListener(MoveBack);
                nextMoveButton.onClick.RemoveListener(MoveForward);
            }

            RegisterOnClickBuyButtons(register);
        }

        private void RegisterOnClickBuyButtons(bool register)
        {
            for (byte i = 0; i < LENGHT_OF_TABLE; i++)
            {
                if (register)
                {
                    itemShopUI[i].BuyButton.onClick.AddListener(menBought[_focusedTableIndex + i]);
                }
                else
                {
                    itemShopUI[i].BuyButton.onClick.RemoveListener(menBought[_focusedTableIndex + i]);
                }
            }
        }

        #endregion

        #region UI

        private void UpdateUI(byte? index = null)
        {
            RegisterOnClickBuyButtons(false);
            _focusedTableIndex = (byte)(index == null ? _focusedTableIndex : index);
            InitializeUI();
            RegisterOnClickBuyButtons(true);
        }

        #endregion

        #region CALLBACKS

        private void BuyMan(WeaponInstance weaponInstance)
        {
            if (playerManager.Player.Money >= weaponInstance.Price)
            {
                playerManager.Player.ReduceMoney(weaponInstance.Price);
                playerManager.Player.AddDamage(weaponInstance.Damage);
                playerManager.Player.AddAutoDamage(weaponInstance.AutoDamage);

                manBought.Invoke();
            }
        }

        private void SwitchSwordsAndMen()
        {
            for (int i = 0; i < LENGHT_OF_IDENTICAL_OBJECTS; i++)
            {
                (weapons[LENGHT_OF_IDENTICAL_OBJECTS + i], weapons[i]) = (weapons[i], weapons[LENGHT_OF_IDENTICAL_OBJECTS + i]);
            }

            UpdateUI();
        }

        private void MoveBack()
        {
            _backMoveGameObject.SetActive(false);
            _nextMoveGameObject.SetActive(true);

            UpdateUI(0);
        }

        private void MoveForward()
        {
            _backMoveGameObject.SetActive(true);
            _nextMoveGameObject.SetActive(false);

            UpdateUI(3);
        }

        #endregion
    }
}