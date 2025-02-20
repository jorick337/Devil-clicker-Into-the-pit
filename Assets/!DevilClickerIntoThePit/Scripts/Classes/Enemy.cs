using UnityEngine;

namespace Game.Classes
{
    [CreateAssetMenu(fileName = "Enemy_1_1", menuName = "Enemy/Create New Enemy", order = 51)]
    public class Enemy : ScriptableObject
    {
        public int Health;
        public ushort Money;
    }
}