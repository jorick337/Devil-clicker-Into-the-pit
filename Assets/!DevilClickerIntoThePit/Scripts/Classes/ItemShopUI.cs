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
        [SerializeField] private Text nameText;
        [SerializeField] private Text damageText;
        [SerializeField] private Text priceText;
        [SerializeField] private Image image;

        #endregion

        #region INITIALIZATION

        public void Initialize(WeaponInstance man)
        {
            UpdateDamageTextAndTransformImage(man.Damage, man.AutoDamage);

            UpdateNameText(man.Name);
            UpdatePriceText(man.Price);

            UpdateSpriteImage(man.Sprite);
        }

        #endregion

        #region UI

        private void UpdateDamageTextAndTransformImage(int damage, int autoDamage)
        {
            if (damage == 0)
            {
                damageText.text = $"{autoDamage} урона в секунду";
                image.rectTransform.sizeDelta = new(35, image.rectTransform.sizeDelta.y);
            }
            else
            {
                damageText.text = $"{damage} урона к клику";
                image.rectTransform.sizeDelta = new(65, image.rectTransform.sizeDelta.y);
            }
        }

        private void UpdateNameText(string value) => nameText.text = value;
        private void UpdatePriceText(int value) => priceText.text = $"{value - 0.01f} $";

        private void UpdateSpriteImage(Sprite sprite) => image.sprite = sprite;

        #endregion
    }
}