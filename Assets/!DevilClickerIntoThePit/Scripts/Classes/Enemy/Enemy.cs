using UnityEngine;
using UnityEngine.UI;

namespace Game.Classes
{
    [CreateAssetMenu(fileName = "Enemy_1_1", menuName = "Enemy/Create New Enemy", order = 51)]
    public class Enemy : ScriptableObject
    {
        public int Health;
        public ushort Money;

        public Image DevilImage;

        public EnemyInstance CreateInstance()
        {
            return new(this);
        }
    }
}