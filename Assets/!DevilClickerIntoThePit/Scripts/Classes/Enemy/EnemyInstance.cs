using UnityEngine;

namespace Game.Classes
{
    public class EnemyInstance
    {
        public int Health { get; private set; }
        public ushort Reward { get; private set; }

        public ushort Price { get; private set; }

        public Sprite DevilSprite { get; private set; }

        public EnemyInstance(Enemy enemy)
        {
            Health = enemy.Health;
            Reward = enemy.Reward;
            Price = enemy.Price;
            DevilSprite = Resources.Load<Sprite>(enemy.PathToSprite);
        }

        public void ReduceHealth(int value) => Health -= value;
        
        public void MultiplyHealth(int value) => Health *= value;
    }
}