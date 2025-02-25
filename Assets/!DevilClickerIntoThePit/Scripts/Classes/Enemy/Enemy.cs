using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName = "Enemy_1_1", menuName = "Enemy/Create New Enemy", order = 51)]
    public class Enemy : ScriptableObject
    {
        [Header("Core")]
        public int Health;
        public ushort Reward;

        public ushort Price;

        [Header("UI")]
        public Sprite DevilSprite;

        public EnemyInstance CreateInstance(ushort numberOfExorcisedDevils = 0)
        {
            EnemyInstance enemyInstance = new(this);

            enemyInstance.MultiplyHealth(numberOfExorcisedDevils == 0 ? 1 : numberOfExorcisedDevils + 1);

            return enemyInstance;
        }
    }
}