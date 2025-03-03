using UnityEngine;

namespace Game.Classes
{
    public class WeaponInstance
    {
        public string Name { get; private set; }
        public ushort Price { get; private set; }

        public ushort Damage { get; private set; }
        public ushort AutoDamage { get; private set; }

        public Sprite Sprite;

        public WeaponInstance(Weapon weapon)
        {
            Name = weapon.Name;
            Price = weapon.Price;

            AutoDamage = weapon.AutoDamage;
            Damage = weapon.Damage;
            
            Sprite = Resources.Load<Sprite>(weapon.PathToSprite);
        }
    }
}