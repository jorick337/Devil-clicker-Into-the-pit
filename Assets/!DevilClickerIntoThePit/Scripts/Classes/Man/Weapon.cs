using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create New Weapon", order = 52)]
    public class Weapon : ScriptableObject
    {
        [Header("Core")]
        public string Name;
        public ushort Price;

        [Header("Damage")]
        public ushort AutoDamage;
        public ushort Damage;

        [Header("UI")]
        public Sprite Sprite;

        public WeaponInstance GetInstance() => new(this);
    }
}