using System;
using YG;

namespace Game.Classes.Player
{
    [Serializable]
    public class Player
    {
        #region CONSTANTS

        private byte MAX_INDEX_OF_LEVELS = 5;

        #endregion

        #region CORE

        public int Money { get; private set; }
        public ushort[] Souls { get; private set; }

        public byte MaxLevelOfDevil { get; private set; }
        public byte MaxLevelOfPit { get; private set; }
        public ushort NumberOfExorcisedDevils { get; private set; }

        public int Damage { get; private set; }
        public int AutoDamage { get; private set; }
        public int DevilPower { get; private set; }

        #endregion

        #region CONSTRUCTS

        public static explicit operator Player(SavesYG savesYG)
        {
            return new Player()
            {
                Money = savesYG.Money,
                Souls = savesYG.Souls,

                MaxLevelOfDevil = savesYG.MaxLevelOfDevil,
                MaxLevelOfPit = savesYG.MaxLevelOfPit,
                NumberOfExorcisedDevils = savesYG.NumberOfExorcisedDevils,

                Damage = savesYG.Damage,
                AutoDamage = savesYG.AutoDamage,
                DevilPower = savesYG.DevilPower
            };
        }

        #endregion

        #region CORE LOGIC

        public void BuyManOrSword(WeaponInstance weaponInstance)
        {
            ReduceMoney(weaponInstance.Price);
            AddDamage(weaponInstance.Damage);
            AddAutoDamage(weaponInstance.AutoDamage);

            Save();
        }

        public void BuyDevil(WeaponInstance weaponInstance, byte index)
        {
            ReduceSouls(weaponInstance.Price, index);
            AddDevilPower(weaponInstance.DevilPower);

            Save();
        }

        public void BuyLevelOfDevil(int price)
        {
            ReduceMoney(price);
            AddMaxLevelOfDevil();

            Save();
        }

        public void BuyLevelOfPit(int price)
        {
            ReduceMoney(price);
            AddMaxLevelOfPit();

            Save();
        }

        public void AddDeathDevil(ushort reward, byte indexSoul)
        {
            AddMoney(reward);
            AddExorcisedDevil();
            AddSoul(indexSoul);

            Save();
        }

        private void Save()
        {
            YandexGame.savesData += this;
        }

        #endregion

        #region ADD

        public void AddMoney(int value) => Money += value;
        public void AddSoul(byte index) => Souls[index] += 1;

        public void AddMaxLevelOfDevil() => MaxLevelOfDevil += 1;
        public void AddMaxLevelOfPit() => MaxLevelOfPit += (byte)(MaxLevelOfPit == MAX_INDEX_OF_LEVELS ? 0 : 1);
        public void AddExorcisedDevil() => NumberOfExorcisedDevils += 1;

        public void AddDamage(int value) => Damage += value;

        public void AddAutoDamage(int value) => AutoDamage += value;

        public void AddDevilPower(int value) => DevilPower += value;

        #endregion

        #region REDUCE

        public void ReduceMoney(int value) => Money -= value;
        public void ReduceSouls(ushort value, byte index) => Souls[index] -= value;

        #endregion
    }
}