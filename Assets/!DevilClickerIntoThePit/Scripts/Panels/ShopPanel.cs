using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels
{
    public class ShopPanel : MonoBehaviour
    {
        #region CONSTANTS

        private const byte LENGHT_TABLE_OF_SHOP = 3;

        #endregion

        #region CORE

        [Header("Core")]
        [SerializeField] private Man[] men;

        [Header("Moving")]
        [SerializeField] private Button backMove;
        [SerializeField] private Button nextMove;

        #endregion

        #region MONO

        private void Awake()
        {
            RegisterEvents(true);
        }

        private void Disable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                backMove.onClick.AddListener(MoveBack);
                nextMove.onClick.AddListener(MoveForward);
            }
            else
            {
                backMove.onClick.RemoveListener(MoveBack);
                nextMove.onClick.RemoveListener(MoveForward);
            }
        }

        #endregion

        #region CALLBACKS

        private void ChangeActiveItemsOfShop(byte index)
        {
            int j = 0;
            for (int i = 0; i < men.Length; i++)
            {
                bool active = j < LENGHT_TABLE_OF_SHOP && i >= index;
                j = active ? j+1 : j;

                men[i].ItemGameObject.SetActive(active);
            }
        }
        
        private void MoveBack() => ChangeActiveItemsOfShop(0);
        private void MoveForward() => ChangeActiveItemsOfShop(2);

        #endregion
    }
}