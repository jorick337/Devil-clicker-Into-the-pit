using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    public class EnemyInstance
    {
        public int Health;
        public ushort Money;

        public Image DevilImage;

        public EnemyInstance(Enemy enemy)
        {
            Health = enemy.Health;
            Money = enemy.Money;
            DevilImage = enemy.DevilImage;
        }
    }
}