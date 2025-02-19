using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels
{
    public class ShopPanel : MonoBehaviour
    {
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

        private void Start()
        {
            backMove.enabled = false;
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

        private void ChangeItemsOfShop(byte index)
        {
            for (int i = 0; i < men.Length; i++)
            {
                if (i > index)
                {
                    men[i].ItemGameObject.SetActive(true);
                }
                else 
                {
                    men[i].ItemGameObject.SetActive(false);
                }
            }
        }
        
        private void MoveBack() => ChangeItemsOfShop(0);
        private void MoveForward() => ChangeItemsOfShop(3);

        #endregion
    }
}