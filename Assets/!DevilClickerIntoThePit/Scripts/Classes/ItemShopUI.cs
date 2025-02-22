using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class ItemShopUI : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private GameObject itemGameObject;
        [SerializeField] private Button buyButton;

        public GameObject ItemGameObject => itemGameObject;
        public Button BuyButton => buyButton;

        [Header("Info")]
        [SerializeField] private Text NameText;
        [SerializeField] private Text DamageText;
        [SerializeField] private Text PriceText;
        [SerializeField] private Image image;

        #endregion

        #region INITIALIZATION

        public void Initialize(WeaponInstance man)
        {
            UpdateDamageText(man.Damage, man.AutoDamage);

            UpdateNameText(man.Name);
            UpdatePriceText(man.Price);

            UpdateSpriteImage(man.Sprite);
        }

        #endregion

        #region UI

        private void UpdateDamageText(int damage, int autoDamage) 
        {
            if (damage == 0)
            {
                DamageText.text = $"{autoDamage} урона в секунду";
            }
            else
            {
                DamageText.text = $"{damage} урона к клику";
            }
        } 

        private void UpdateNameText(string value) => NameText.text = value;
        private void UpdatePriceText(int value) => PriceText.text = $"{value - 0.01f} $";

        private void UpdateSpriteImage(Sprite sprite) => image.sprite = sprite;

        #endregion
    }
}