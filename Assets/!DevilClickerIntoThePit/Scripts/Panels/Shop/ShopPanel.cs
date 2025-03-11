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
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            UpdateItems();
        }

        private void OnEnable()
        {
            movingShopPanel.ItemsSwitched += SwitchSwordsAndMen;
            movingShopPanel.PastItemsMoved += MoveBack;
            movingShopPanel.NextItemsMoved += MoveForward;

            settingsPanel.DiggingAndDevilSpasesChanged += SwitchDiggingAndDevilSpases;

            playerManager.PitClosed += UndoChangesByDiggingSpase;

            RegisterOnClickBuyButtons(true);
        }

        private void OnDisable()
        {
            movingShopPanel.ItemsSwitched -= SwitchSwordsAndMen;
            movingShopPanel.PastItemsMoved -= MoveBack;
            movingShopPanel.NextItemsMoved -= MoveForward;

            settingsPanel.DiggingAndDevilSpasesChanged -= SwitchDiggingAndDevilSpases;

            playerManager.PitClosed -= UndoChangesByDiggingSpase;

            RegisterOnClickBuyButtons(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            menBought = new UnityAction[weapons.Length];
            for (byte i = 0; i < weapons.Length; i++)
            {
                byte index = i;
                byte indexSoul = (byte)(index % LENGHT_OF_IDENTICAL_OBJECTS);

                menBought[i] = () => BuyWeapon(weapons[index].GetInstance(), indexSoul);
            }

            _focusedTableIndex = 0;
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

        private void BuyWeapon(WeaponInstance weaponInstance, byte index)
        {
            if (!enemyManager.IsDiggingSpaseActive)
            {
                if (playerManager.Player.Money >= weaponInstance.Price)
                {
                    playerManager.Player.BuyManOrSword(weaponInstance);
                    manBought.Invoke();
                }
            }
            else
            {
                ushort countSoul = playerManager.Player.Souls[index];
                if (countSoul >= weaponInstance.Price)
                {
                    byte pastIndex = enemyManager.SelectedIndexDevil;

                    playerManager.Player.BuyDevil(weaponInstance, index);

                    enemyManager.SetSelectedIndexDevil(index);
                    manBought.Invoke();
                    enemyManager.SetSelectedIndexDevil(pastIndex);
                }
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

        private void UndoChangesByDiggingSpase()
        {
            while (weapons[0].DevilPower != 0) // disable devils from shop
            {
                SwitchDiggingAndDevilSpases();
            }
            settingsPanel.DiggingAndDevilSpasesChanged -= SwitchDiggingAndDevilSpases;
        }

        private void MoveBack() => UpdateUI(0);
        private void MoveForward() => UpdateUI(3);

        #endregion
    }
}