using Game.Managers;
using Game.Panels.Characteristics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Panels
{
    public class EnemyPanel : MonoBehaviour
    {
        #region CORE

        [Header("UI")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Text healthText;
        [SerializeField] private Image enemyImage;

        private Tween _devilAnimation;
        private Sequence _healthAnimation;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

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
            _devilAnimation = enemyImage.transform.DOScaleY(0.89f, 0.1f).From(1).SetAutoKill(false).Pause();

            _healthAnimation = DOTween.Sequence()
                .Append(healthSlider.transform.DORotate(new Vector3(0,0,5), 0.05f).From(new Vector3(0,0,0)))
                .Append(healthSlider.transform.DORotate(new Vector3(0,0,-5), 0.1f).From(new Vector3(0,0,5)))
                .Append(healthSlider.transform.DORotate(new Vector3(0,0,0), 0.05f).From(new Vector3(0,0,0)))
                .SetAutoKill(false).Pause();
        }

        public void InitializeUI()
        {
            UpdateEnemySprite();
            UpdateHealthText();
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
            _devilAnimation.Restart();
            UpdateHealthText();

            _healthAnimation.Restart();
            UpdateValueHealthSlider();
        }

        #endregion
    }
}