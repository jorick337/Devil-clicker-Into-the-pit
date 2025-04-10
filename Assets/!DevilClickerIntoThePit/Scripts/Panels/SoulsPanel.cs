using Game.Classes;
using Game.Managers;
using Game.Panels.Characteristics;
using UnityEngine;

namespace Game.Panels
{
    public class SoulsPanel : MonoBehaviour
    {
        #region CONSTANTS

        private string PATH_TO_SOULS = "Enemy/Souls/Soul"; // Только прибавить номер(от 1 до 6)

        #endregion

        #region CORE 

        private SoulUI[] soulUIs;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;
        [SerializeField] private ShopPanel shopPanel;

        [Header("Managers")]
        [SerializeField] private EnemyManager enemyManager;

        private PlayerManager _playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            _playerManager = PlayerManager.Instance;

            InitializeValues();
            InitializeUI();
        }

        private void OnEnable()
        {
            improvedDevilPanel.EnemyImproved += InitializeUI;
            shopPanel.ManBought += UpdateCountSoul;

            enemyManager.DevilBanished += UpdateCountSoul;
            _playerManager.PitClosed += UndoChangesByDiggingSpase;
        }

        private void OnDisable()
        {
            improvedDevilPanel.EnemyImproved -= InitializeUI;
            shopPanel.ManBought -= UpdateCountSoul;

            enemyManager.DevilBanished -= UpdateCountSoul;
            _playerManager.PitClosed -= UndoChangesByDiggingSpase;
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            soulUIs = new SoulUI[6];
        }

        private void InitializeUI()
        {
            ushort[] souls = _playerManager.Player.Souls;
            for (int i = transform.childCount; i < _playerManager.Player.MaxLevelOfDevil; i++)
            {
                if (souls[i] >= 0)
                {
                    SoulUI soulUI = Resources.Load<SoulUI>($"{PATH_TO_SOULS} {i + 1}");
                    soulUI = Instantiate(soulUI, transform);

                    soulUIs[i] = soulUI;
                }
            }
        }

        #endregion

        #region UI

        private void UpdateCountSoul()
        {
            soulUIs[enemyManager.SelectedIndexDevil].InitializeUI();
        }

        #endregion

        #region CALLBACKS

        private void UndoChangesByDiggingSpase()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}