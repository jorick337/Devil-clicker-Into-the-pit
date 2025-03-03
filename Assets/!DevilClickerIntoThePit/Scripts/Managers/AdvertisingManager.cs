using Game.Panels.Characteristics;
using UnityEngine;
using YG;

namespace Game.Managers
{
    public class AdvertisingManager : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private byte chance;

        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        private void Awake()
        {
            RegisterEvents(true);
        }

        private void OnDisable()
        {
            RegisterEvents(false);
        }

        private void RegisterEvents(bool register)
        {
            if (register)
            {
                improvedDevilPanel.EnemyImproved += YandexGame.FullscreenShow;
            }
            else
            {
                improvedDevilPanel.EnemyImproved -= YandexGame.FullscreenShow;
            }
        }
    }
}