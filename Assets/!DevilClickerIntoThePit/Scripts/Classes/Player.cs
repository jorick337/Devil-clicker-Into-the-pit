namespace Game.Classes.Player
{
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

        public Player()
        {
            Money = 10000000;
            Souls = new ushort[6] { 0, 0, 0, 0, 0, 0 };

            MaxLevelOfDevil = 1;
            MaxLevelOfPit = 0;
            NumberOfExorcisedDevils = 0;

            Damage = 1;
            AutoDamage = 0;
            DevilPower = 0;
        }

        #endregion

        #region CORE LOGIC

        public void BuyManOrSword(WeaponInstance weaponInstance)
        {
            ReduceMoney(weaponInstance.Price);
            AddDamage(weaponInstance.Damage);
            AddAutoDamage(weaponInstance.AutoDamage);
        }

        public void BuyDevil(WeaponInstance weaponInstance, byte index)
        {
            ReduceSouls(weaponInstance.Price, index);
            AddDevilPower(weaponInstance.DevilPower);
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