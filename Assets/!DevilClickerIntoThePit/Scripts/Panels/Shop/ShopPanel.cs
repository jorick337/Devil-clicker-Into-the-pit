using System;
using Game.Classes;
using Game.Managers;
using Game.Panels.Shop;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Panels
{
    public class ShopPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const byte LENGHT_OF_TABLE = 3;
        private const byte LENGHT_OF_IDENTICAL_OBJECTS = 6;

        private const byte START_INDEX_OF_DEVILS = 12;

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

        [Header("Panels")]
        [SerializeField] private MovingShopPanel movingShopPanel;
        [SerializeField] private SettingsPanel settingsPanel;

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
            UpdateItems();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                movingShopPanel.ItemsSwitched += SwitchSwordsAndMen;
                movingShopPanel.PastItemsMoved += MoveBack;
                movingShopPanel.NextItemsMoved += MoveForward;

                settingsPanel.DiggingAndDevilSpasesChanged += SwitchDiggingAndDevilSpases;
            }
            else
            {
                movingShopPanel.ItemsSwitched -= SwitchSwordsAndMen;
                movingShopPanel.PastItemsMoved -= MoveBack;
                movingShopPanel.NextItemsMoved -= MoveForward;

                settingsPanel.DiggingAndDevilSpasesChanged -= SwitchDiggingAndDevilSpases;
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
            UpdateItems();

            RegisterOnClickBuyButtons(true);
        }

        private void UpdateItems()
        {
            for (byte i = 0; i < LENGHT_OF_TABLE; i++)
            {
                itemShopUI[i].InitializeUI(weapons[_focusedTableIndex + i].GetInstance());
            }
        }

        private void SwitchFirstAndOtherWeapons(byte lenght, byte startIndex)
        {
            for (byte i = 0; i < lenght; i++)
            {
                (weapons[startIndex + i], weapons[i]) = (weapons[i], weapons[startIndex + i]);
            }
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
            SwitchFirstAndOtherWeapons(LENGHT_OF_IDENTICAL_OBJECTS, LENGHT_OF_IDENTICAL_OBJECTS);
            UpdateUI();
        }

        private void SwitchDiggingAndDevilSpases()
        {
            SwitchFirstAndOtherWeapons(LENGHT_OF_IDENTICAL_OBJECTS, START_INDEX_OF_DEVILS);
            UpdateUI();
        }

        private void MoveBack() => UpdateUI(0);
        private void MoveForward() => UpdateUI(3);

        #endregion
    }
}