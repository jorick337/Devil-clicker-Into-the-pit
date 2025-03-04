using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class SoulUI : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private Text countText;
        [SerializeField] private byte index;

        public Text text => countText;

        private void Awake()
        {
            InitializeUI();
        }

        public void InitializeUI()
        {
            countText.text = PlayerManager.Instance.Player.Souls[index].ToString();
        }
    }
}