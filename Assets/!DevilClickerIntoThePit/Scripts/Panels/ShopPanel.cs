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
        [SerializeField] private Man[] men;

        [Header("Moving")]
        [SerializeField] private Button backMove;
        [SerializeField] private Button nextMove;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            RegisterEvents(true);
        }

        private void Disable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            menBought = new UnityAction[men.Length];
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                backMove.onClick.AddListener(MoveBack);
                nextMove.onClick.AddListener(MoveForward);

                for (int i = 0; i < men.Length; i++)
                {
                    menBought[i] = () => BuyMan(men[i]);
                    men[i].Button.onClick.AddListener(menBought[i]);
                }
            }
            else
            {
                backMove.onClick.RemoveListener(MoveBack);
                nextMove.onClick.RemoveListener(MoveForward);

                for (int i = 0; i < men.Length; i++)
                {
                    men[i].Button.onClick.RemoveListener(menBought[i]);
                }
            }
        }

        #endregion

        #region CALLBACKS

        private void BuyMan(Man man)
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

                men[i].ItemGameObject.SetActive(active);
            }
        }

        private void MoveBack() => ChangeActiveItemsOfShop(0);
        private void MoveForward() => ChangeActiveItemsOfShop(2);

        #endregion
    }
}