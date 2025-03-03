using System;
using YG;

namespace Game.Classes
{
    [Serializable]
    public class Player
    {
        #region CORE

        public int Money;

        public byte LevelOfDevil;
        public ushort NumberOfExorcisedDevils;

        public int Damage;
        public int AutoDamage;

        #endregion

        #region CONSTRUCTS

        public static explicit operator Player(SavesYG savesYG)
        {
            return new Player()
            {
                Money = savesYG.Money,

                LevelOfDevil = savesYG.LevelOfDevil,
                NumberOfExorcisedDevils = savesYG.NumberOfExorcisedDevils,

                Damage = savesYG.Damage,
                AutoDamage = savesYG.AutoDamage
            };
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