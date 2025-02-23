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

        private const byte LENGHT_TABLE_OF_SHOP = 3;
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
            _focusedTableIndex = 0;
        }

        private void InitializeUI()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                itemShopUI[i].Initialize(weapons[i].GetInstance());
            }
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                switchSwordsAndMenButton.onClick.AddListener(SwitchSwordsAndMen);
                backMoveButton.onClick.AddListener(MoveBack);
                nextMoveButton.onClick.AddListener(MoveForward);

                for (byte i = 0; i < weapons.Length; i++)
                {
                    byte index = i;

                    menBought[i] = () => BuyMan(weapons[index].GetInstance());
                    itemShopUI[i].BuyButton.onClick.AddListener(menBought[i]);
                }
            }
            else
            {
                switchSwordsAndMenButton.onClick.RemoveListener(SwitchSwordsAndMen);
                backMoveButton.onClick.RemoveListener(MoveBack);
                nextMoveButton.onClick.RemoveListener(MoveForward);

                for (int i = 0; i < weapons.Length; i++)
                {
                    itemShopUI[i].BuyButton.onClick.RemoveListener(menBought[i]);
                }
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
            for (int i = 0; i < LENGHT_OF_IDENTICAL_OBJECTS; i++)
            {
                (itemShopUI[LENGHT_OF_IDENTICAL_OBJECTS + i], itemShopUI[i]) = (itemShopUI[i], itemShopUI[LENGHT_OF_IDENTICAL_OBJECTS + i]);
            }

            ChangeActiveItemsOfShop();
        }

        private void ChangeActiveItemsOfShop()
        {
            int j = 0;
            for (int i = 0; i < weapons.Length; i++)
            {
                bool active = j < LENGHT_TABLE_OF_SHOP && i >= _focusedTableIndex;
                j = active ? j + 1 : j;

                itemShopUI[i].ItemGameObject.SetActive(active);
            }
        }

        private void MoveBack() 
        {
            _backMoveGameObject.SetActive(false);
            _nextMoveGameObject.SetActive(true);

            _focusedTableIndex = 0;
            ChangeActiveItemsOfShop();
        } 

        private void MoveForward() 
        {
            _backMoveGameObject.SetActive(true);
            _nextMoveGameObject.SetActive(false);

            _focusedTableIndex = 3;
            ChangeActiveItemsOfShop();
        }

        #endregion
    }
}