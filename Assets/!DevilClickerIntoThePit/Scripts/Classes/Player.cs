using System;

namespace Game.Classes
{
    public class Player
    {
        #region CORE

        public int Money { get; private set; }

        public byte LevelOfDevil { get; private set; }
        public ushort NumberOfExorcisedDevils { get; private set; }

        public int Damage { get; private set; }
        public int AutoDamage { get; private set; }

        #endregion

        #region CONSTRUCTS

        public Player()
        {
            Money = 0;

            LevelOfDevil = 1;
            NumberOfExorcisedDevils = 0;
            
            Damage = 1;
            AutoDamage = 0;
        }

        #endregion

        #region ADD

        public void AddMoney(int value) => Money += value;
        
        public void AddLevelOfDevil() => LevelOfDevil += 1;
        public void AddExorcisedDevil() => NumberOfExorcisedDevils += 1;
        
        public void AddDamage(int value) => Damage += value;
        public void AddAutoDamage(int value) => AutoDamage += value;

        #endregion

        #region REDUCE

        public void ReduceMoney(int value) => Money -= value;

        #endregion
    }
}