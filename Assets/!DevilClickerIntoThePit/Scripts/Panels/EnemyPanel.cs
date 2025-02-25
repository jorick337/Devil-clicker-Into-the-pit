using Game.Managers;
using Game.Panels.Characteristics;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels
{
    public class EnemyPanel : MonoBehaviour
    {
        #region CONSTANTS

        private string ANIMATION_DEVIL_CLICK = "Click";
        private string ANIMATION_DEVIL_LOST_HEALTH = "LossHealth";

        #endregion

        #region CORE

        [Header("UI")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Text healthText;
        [SerializeField] private Image enemyImage;

        [Header("Animations")]
        [SerializeField] private Animator devilAnimator;
        [SerializeField] private Animator healthAnimator;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeUI();
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        #endregion

        #region INITIALIZATION

        public void InitializeUI()
        {
            UpdateEnemySprite();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += InitializeUI;
                improvedDevilPanel.EnemyImproved += UpdateHealthText;
                improvedDevilPanel.EnemyImproved += UpdateValueHealthSlider;

                enemyManager.HealthChanged += StartChangingHealthEnemy;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= InitializeUI;
                improvedDevilPanel.EnemyImproved -= UpdateHealthText;
                improvedDevilPanel.EnemyImproved -= UpdateValueHealthSlider;

                enemyManager.HealthChanged -= StartChangingHealthEnemy;
            }
        }

        #endregion

        #region UI

        private void UpdateValueHealthSlider() => healthSlider.value = enemyManager.GetPercentageOfHealth();
        private void UpdateHealthText() => healthText.text = enemyManager.SelectableEnemy.Health.ToString();

        private void UpdateEnemySprite() => enemyImage.sprite = enemyManager.SelectableEnemy.DevilSprite;

        #endregion

        #region CALLBACKS

        private void StartChangingHealthEnemy()
        {
            devilAnimator.Play(ANIMATION_DEVIL_CLICK);
            UpdateHealthText();

            healthAnimator.Play(ANIMATION_DEVIL_LOST_HEALTH);
            UpdateValueHealthSlider();
        }

        #endregion
    }
}