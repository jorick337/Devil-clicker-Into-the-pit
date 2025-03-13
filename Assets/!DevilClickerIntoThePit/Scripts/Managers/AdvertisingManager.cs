using Game.Panels.Characteristics;
using UnityEngine;

namespace Game.Managers
{
    public class AdvertisingManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private ImprovedDevilPanel improvedDevilPanel;

        private void OnEnable()
        {
            // improvedDevilPanel.EnemyImproved += ;
        }

        private void OnDisable()
        {
            // improvedDevilPanel.EnemyImproved -= ;
        }
    }
}