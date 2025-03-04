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
        [SerializeField] private Image image;

        [Header("Image")]
        [SerializeField] private ushort sizeDeltaXSwordImage;
        [SerializeField] private ushort sizeDeltaXManImage;

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
            UpdateDamageTextAndTransformImage(weaponInstance.Damage, weaponInstance.AutoDamage);

            UpdateNameText(weaponInstance.Name);
            UpdatePriceText(weaponInstance.Price);

            UpdateSpriteImage(weaponInstance.Sprite);
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

        private void UpdateDamageTextAndTransformImage(int damage, int autoDamage)
        {
            if (damage == 0)
            {
                damageText.text = $"{autoDamage} урона в секунду";
                image.rectTransform.sizeDelta = new(sizeDeltaXManImage, image.rectTransform.sizeDelta.y);
            }
            else
            {
                damageText.text = $"{damage} урона к клику";
                image.rectTransform.sizeDelta = new(sizeDeltaXSwordImage, image.rectTransform.sizeDelta.y);
            }
        }

        private void UpdateNameText(string value) => nameText.text = value;
        private void UpdatePriceText(int value) => priceText.text = $"{value - 0.01f} $";

        private void UpdateSpriteImage(Sprite sprite) => image.sprite = sprite;

        #endregion

        #region CALLBACKS

        private void ShowClickButton() => clickAnimation.Restart();

        #endregion
    }
}