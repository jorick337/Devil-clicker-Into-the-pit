using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class Man : MonoBehaviour
    {
        [Header("Core")]
        public ushort Damage { get; private set; }
        public ushort Price { get; private set; }
        
        [Header("UI")]
        public GameObject ItemGameObject { get; private set; }
        public Button Button { get; private set; }
    }
}