using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class Man : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] private ushort damage;
        [SerializeField] private ushort price;

        public ushort Damage => damage;
        public ushort Price => price;
        
        [Header("UI")]
        [SerializeField] private GameObject itemGameObject;
        [SerializeField] private Button button;

        public GameObject ItemGameObject => itemGameObject;
        public Button Button => button;
    }
}