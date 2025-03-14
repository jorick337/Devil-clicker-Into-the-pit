using Game.Managers;
using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName = "ImprovingSettings", menuName = "ImprovePanel/Create New Settings", order = 53)]
    public class ImprovingSettings : ScriptableObject
    {
        [Header("Core")]
        [SerializeField] private float[] percentagesOfPitHealth;
        [SerializeField] private int[] prices;

        public int GetPrice()
        {
            int index = PlayerManager.Instance.Player.MaxLevelOfPit;

            return index >= prices.Length ? 0 : prices[index] ;
        }

        public float GetPercentage()
        {
            int index = PlayerManager.Instance.Player.MaxLevelOfPit;

            return percentagesOfPitHealth[index];
        }
    }
}