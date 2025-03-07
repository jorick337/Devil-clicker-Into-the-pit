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
        [SerializeField] private ShopPanel shopPanel;

        private MovingEnemyPanel _movingEnemyPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private PlayerManager playerManager;

        [Header("Colors")]
        [SerializeField] private Color enableDiggingSpaseSliderColor;
        [SerializeField] private Color enableDevilSpaseSliderColor;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeAnimations();
            InitializeUI();
        }

        private void OnEnable()
        {
            devilButton.onClick.AddListener(enemyManager.TakeDamage);

            improvedDevilPanel.EnemyImproved += InitializeUI;
            improvedDevilPanel.PitImproved += UpdateValueHealthSlider;
            improvedDevilPanel.PitImproved += UpdateHealthText;

            settingsPanel.DiggingAndDevilSpasesChanged += ToggleDiggingAndDevilsSpases;
            shopPanel.manBought += InitializeUI;

            enemyManager.HealthChanged += StartChangingHealthEnemy;
            enemyManager.DevilChanged += InitializeUI;
        }

        private void OnDisable()
        {
            devilButton.onClick.RemoveListener(enemyManager.TakeDamage);
            
            improvedDevilPanel.EnemyImproved -= InitializeUI;
            improvedDevilPanel.PitImproved -= UpdateValueHealthSlider;
            improvedDevilPanel.PitImproved -= UpdateHealthText;

            settingsPanel.DiggingAndDevilSpasesChanged -= ToggleDiggingAndDevilsSpases;
            shopPanel.manBought -= InitializeUI;

            enemyManager.HealthChanged -= StartChangingHealthEnemy;
            enemyManager.DevilChanged -= InitializeUI;
        }

        #endregion

        #region INITIALIZATION

        private void InitializeAnimations()
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

        private void RegisterMovingEnemyPanelEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += _movingEnemyPanel.SwitchActiveButtons;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= _movingEnemyPanel.SwitchActiveButtons;
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
            if (enemyManager.IsDiggingSpaseActive)
            {
                if (_movingEnemyPanel != null)
                {
                    RegisterMovingEnemyPanelEvents(false);

                    Destroy(_movingEnemyPanel.gameObject);
                    _movingEnemyPanel = null;

                    Resources.UnloadUnusedAssets();
                }
            }
            else if (playerManager.Player.MaxLevelOfDevil > 0 && _movingEnemyPanel == null)
            {
                _movingEnemyPanel = movingPanelResource.GetInstantiateGameObject<MovingEnemyPanel>();
                RegisterMovingEnemyPanelEvents(true);
            }
        }

        private void UpdateHealthText() => healthText.text = enemyManager.GetHealth();
        private void UpdateValueHealthSlider() => healthSlider.value = enemyManager.GetPercentageOfHealth();

        #endregion

        #region CALLBACKS

        private void StartChangingHealthEnemy()
        {
            if (!enemyManager.IsDiggingSpaseActive)
            {
                _devilAnimation.Restart();
                UpdateHealthText();

                _healthAnimation.Restart();
                UpdateValueHealthSlider();
            }
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