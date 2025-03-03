using Game.Panels.Characteristics;
using UnityEngine;

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
                //improvedDevilPanel.EnemyImproved += ;
            }
            else
            {
                //improvedDevilPanel.EnemyImproved -= ;
            }
        }

        public void TryShowFullscreenAdWithChance()
        {
            int random = Random.Range(0, 101);

            if (chance < random)
            {
                return;
            }
        }
    }
}