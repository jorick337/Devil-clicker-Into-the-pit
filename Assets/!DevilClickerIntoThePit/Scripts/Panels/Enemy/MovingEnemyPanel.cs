using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Panels.Enemy
{
    public class MovingEnemyPanel : MonoBehaviour
    {
        #region CORE

        [Header("UI")]
        [SerializeField] private Button nextDevilButton;
        [SerializeField] private Button pastDevilButton;

        private PlayerManager _playerManager;
        private EnemyManager _enemyManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeManagers();
            InitializeUI();
        }

        private void OnEnable()
        {
            nextDevilButton.onClick.AddListener(GoNextDevil);
            pastDevilButton.onClick.AddListener(GoPastDevil);
        }

        private void OnDisable()
        {
            nextDevilButton.onClick.RemoveListener(GoNextDevil);
            pastDevilButton.onClick.RemoveListener(GoPastDevil);
        }

        #endregion

        #region INITIALIZATION

        private void InitializeManagers()
        {
            _playerManager = PlayerManager.Instance;
            _enemyManager = EnemyManager.Instance;
        }

        private void InitializeUI()
        {
            SwitchActiveButtons();
        }

        #endregion

        #region UI

        public void SwitchActiveButtons()
        {
            bool nextActive = _enemyManager.SelectedIndexDevil < _playerManager.Player.MaxLevelOfDevil - 1;
            bool pastActive = _enemyManager.SelectedIndexDevil > 0;

            nextDevilButton.gameObject.SetActive(nextActive);
            pastDevilButton.gameObject.SetActive(pastActive);
        }

        #endregion

        #region CALLBACKS

        private void GoNextDevil()
        {
            _enemyManager.AddSelectedIndexDevil();
            SwitchActiveButtons();
        }

        private void GoPastDevil()
        {
            _enemyManager.ReduceSelectedIndexDevil();
            SwitchActiveButtons();
        }

        #endregion
    }
}