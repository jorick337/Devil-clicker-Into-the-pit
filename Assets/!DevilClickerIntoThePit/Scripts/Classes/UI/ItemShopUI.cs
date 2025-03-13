using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class ItemShopUI : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private Button buyButton;

        public Button BuyButton => buyButton;

        private Sequence clickAnimation;

        [Header("Info")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text damageText;
        [SerializeField] private Text priceText;
        [SerializeField] private Image priceImage;
        [SerializeField] private Image itemImage;

        [Header("Image")]
        [SerializeField] private short sizeDeltaXSwordImage;
        [SerializeField] private short sizeDeltaXManImage;
        [SerializeField] private short sizeDeltaDevilImage;

        [Header("Colors")]
        [SerializeField] private Color colorOfDollar;
        [SerializeField] private Color colorOfSoul;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        public void InitializeUI(WeaponInstance weaponInstance)
        {
            UpdateDamageTextAndTransformImage(weaponInstance.Damage, weaponInstance.AutoDamage, weaponInstance.DevilPower);

            UpdateNameText(weaponInstance.Name);
            UpdatePriceText(weaponInstance.Price);

            UpdateItemImage(weaponInstance.ItemSprite);
            UpdatePriceImage(weaponInstance.PriceSprite, weaponInstance.DevilPower > 0);

            Resources.UnloadUnusedAssets();
        }

        private void InitializeValues()
        {
            clickAnimation = DOTween.Sequence()
                .Append(buyButton.transform.DOScaleY(0.89f, 0.1f).From(1f))
                .Append(buyButton.transform.DOScaleY(1f, 0.1f).From(0.89f))
                .SetAutoKill(false).Pause();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                buyButton.onClick.AddListener(ShowClickButton);
            }
            else
            {
                buyButton.onClick.RemoveListener(ShowClickButton);
            }
        }

        #endregion

        #region UI

        private void UpdateDamageTextAndTransformImage(int damage, int autoDamage, int devilPower)
        {
            string text;
            float sizeDeltaX;

            if (autoDamage > 0)
            {
                text = $"{autoDamage} урона в секунду";
                sizeDeltaX = sizeDeltaXManImage;
            }
            else if (damage > 0)
            {
                text = $"{damage} урона к клику";
                sizeDeltaX = sizeDeltaXSwordImage;
            }
            else
            {
                text = $"{devilPower} к рабочей силе";
                sizeDeltaX = sizeDeltaDevilImage;
            }

            damageText.text = text;
            itemImage.rectTransform.sizeDelta = new Vector2(sizeDeltaX, itemImage.rectTransform.sizeDelta.y);
        }

        private void UpdatePriceImage(Sprite sprite, bool isDevil)
        {
            priceImage.sprite = sprite;
            priceImage.color = isDevil ? colorOfSoul : colorOfDollar;
        }

        private void UpdateItemImage(Sprite sprite) => itemImage.sprite = sprite;
        
        private void UpdateNameText(string value) => nameText.text = value;
        private void UpdatePriceText(int value) => priceText.text = value.ToString();

        #endregion

        #region CALLBACKS

        private void ShowClickButton() => clickAnimation.Restart();

        #endregion
    }
}