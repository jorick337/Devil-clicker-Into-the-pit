using Game.Panels.Characteristics;
using UnityEngine;
using YG;

namespace Game.Managers
{
    public class AdvertisingManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        private void OnEnable()
        {
            improvedDevilPanel.EnemyImproved += YandexGame.FullscreenShow;
        }

        private void OnDisable()
        {
            improvedDevilPanel.EnemyImproved -= YandexGame.FullscreenShow;
        }
    }
}