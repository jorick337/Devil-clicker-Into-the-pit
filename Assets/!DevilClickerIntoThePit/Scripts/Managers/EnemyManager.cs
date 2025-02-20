using Game.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region CORE

        [Header("Core")]
        [SerializeField] private Enemy[] enemies;

        private Enemy _enemy;

        [Header("UI")]
        [SerializeField] private Text healthText;
        [SerializeField] private Image enemyImage;

        [Header("Managers")]
        [SerializeField] private PlayerManager playerManager;

        #endregion

        #region MONO

        private void Awake()
        {
            InitializeValues();
        }

        #endregion

        #region INITIALIZATION

        private void InitializeValues()
        {
            _enemy = enemies[playerManager.Player.LevelOfDevil - 1];
        }

        #endregion

        #region UI

        public void TakeDamage(int damage)
        {
            AddDamage(damage);

            if (_enemy.Health < 0)
            {
                playerManager.Player.AddMoney(_enemy.Money);
                

                InitializeValues();
            }
            SetText(_enemy.Health);
        }

        #endregion

        #region SET

        private void SetText(int health) => healthText.text = $"{health}";

        #endregion

        #region ADD

        private void AddDamage(int damage)
        {
            _enemy.Health -= damage;
        }

        #endregion
    }
}