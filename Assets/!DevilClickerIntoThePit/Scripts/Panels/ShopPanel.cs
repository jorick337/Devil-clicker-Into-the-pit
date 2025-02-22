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

        #endregion

        #region EVENTS

        public Action manBought;

        private UnityAction[] menBought;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private WeaponInstance[] men;
        [SerializeField] private ItemShopUI[] itemShopUI;

        [Header("Moving")]
        [SerializeField] private Button backMoveButton;
        [SerializeField] private Button nextMoveButton;

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
            menBought = new UnityAction[men.Length];
        }

        private void InitializeUI()
        {
            for (int i = 0; i < men.Length; i++)
            {
                itemShopUI[i].Initialize(men[i]);
            }
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                backMoveButton.onClick.AddListener(MoveBack);
                nextMoveButton.onClick.AddListener(MoveForward);

                for (byte i = 0; i < men.Length; i++)
                {
                    byte index = i;

                    menBought[i] = () => BuyMan(men[index]);
                    itemShopUI[i].BuyButton.onClick.AddListener(menBought[i]);
                }
            }
            else
            {
                backMoveButton.onClick.RemoveListener(MoveBack);
                nextMoveButton.onClick.RemoveListener(MoveForward);

                for (int i = 0; i < men.Length; i++)
                {
                    itemShopUI[i].BuyButton.onClick.RemoveListener(menBought[i]);
                }
            }
        }

        #endregion

        #region CALLBACKS

        private void BuyMan(WeaponInstance man)
        {
            if (playerManager.Player.Money >= man.Price)
            {
                playerManager.Player.ReduceMoney(man.Price);
                playerManager.Player.AddDamage(man.Damage);

                manBought.Invoke();
            }
        }

        private void ChangeActiveItemsOfShop(byte index)
        {
            int j = 0;
            for (int i = 0; i < men.Length; i++)
            {
                bool active = j < LENGHT_TABLE_OF_SHOP && i >= index;
                j = active ? j + 1 : j;

                itemShopUI[i].ItemGameObject.SetActive(active);
            }
        }

        private void MoveBack() => ChangeActiveItemsOfShop(0);
        private void MoveForward() => ChangeActiveItemsOfShop(3);

        #endregion
    }
}