using Game.Managers;
using Game.Panels.Characteristics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Game.Classes;

namespace Game.Panels.Enemy
{
    public class EnemyPanel : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private Resource movingPanelResource;

        [Header("UI")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image healthImage;
        [SerializeField] private Text healthText;

        [SerializeField] private Button devilButton;
        [SerializeField] private Image enemyImage;

        // Animations
        private Tween _devilAnimation;
        private Tween _healthAnimation;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;
        [SerializeField] private SettingsPanel settingsPanel;

        private MovingEnemyPanel _movingEnemyPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

        [Header("Colors")]
        [SerializeField] private Color enableDiggingSpaseSliderColor;
        [SerializeField] private Color enableDevilSpaseSliderColor;

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
            _devilAnimation = DOTween.Sequence()
                .Append(enemyImage.transform.DOScaleY(0.89f, 0.1f).From(1))
                .Append(enemyImage.transform.DOScaleY(0.89f, 0.1f).From(1))
                .SetAutoKill(false).Pause();

            _healthAnimation = DOTween.Sequence()
                .Append(healthSlider.transform.DORotate(new Vector3(0, 0, 5), 0.05f).From(new Vector3(0, 0, 0)))
                .Append(healthSlider.transform.DORotate(new Vector3(0, 0, -5), 0.1f).From(new Vector3(0, 0, 5)))
                .Append(healthSlider.transform.DORotate(new Vector3(0, 0, 0), 0.05f).From(new Vector3(0, 0, 0)))
                .SetAutoKill(false).Pause();
        }

        private void InitializeUI()
        {
            UpdateEnemySprite();
            UpdateHealthText();
            UpdateValueHealthSlider();
            LoadMovingPanel();
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += InitializeUI;
                settingsPanel.DiggingAndDevilSpasesChanged += ToggleDiggingAndDevilsSpases;

                enemyManager.HealthChanged += StartChangingHealthEnemy;
                enemyManager.DevilChanged += InitializeUI;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= InitializeUI;
                settingsPanel.DiggingAndDevilSpasesChanged -= ToggleDiggingAndDevilsSpases;

                enemyManager.HealthChanged -= StartChangingHealthEnemy;
                enemyManager.DevilChanged -= InitializeUI;
            }
        }

        #endregion

        #region UI

        private void UpdateEnemySprite()
        {
            enemyImage.sprite = enemyManager.SelectableEnemy.DevilSprite;
            Resources.UnloadUnusedAssets();
        }

        private void LoadMovingPanel()
        {
            if (enemyManager.SelectedIndexDevil > 0 && _movingEnemyPanel == null && !enemyManager.IsDiggingSpaseActive)
            {
                _movingEnemyPanel = movingPanelResource.GetInstantiateGameObject<MovingEnemyPanel>();
            }
        }

        private void UpdateHealthText() => healthText.text = enemyManager.GetHealth().ToString();
        private void UpdateValueHealthSlider() => healthSlider.value = enemyManager.GetPercentageOfHealth();

        #endregion

        #region CALLBACKS

        private void StartChangingHealthEnemy()
        {
            _devilAnimation.Restart();
            UpdateHealthText();

            _healthAnimation.Restart();
            UpdateValueHealthSlider();
        }

        private void ToggleDiggingAndDevilsSpases()
        {
            bool isDiggingSpaseActive = enemyManager.IsDiggingSpaseActive;

            devilButton.gameObject.SetActive(!isDiggingSpaseActive);
            healthImage.color = isDiggingSpaseActive ? enableDiggingSpaseSliderColor : enableDevilSpaseSliderColor;

            InitializeUI();
        }

        #endregion
    }
}