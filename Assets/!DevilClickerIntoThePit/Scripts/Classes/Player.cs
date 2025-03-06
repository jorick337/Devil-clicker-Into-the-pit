namespace Game.Classes.Player
{
    public class Player
    {
        #region CORE

        public int Money { get; private set; }
        public ushort[] Souls { get; private set; }

        public byte MaxLevelOfDevil { get; private set; }
        public ushort NumberOfExorcisedDevils { get; private set; }

        public int Damage { get; private set; }
        public int AutoDamage { get; private set; }
        public int DevilPower { get; private set; }

        #endregion

        #region CONSTRUCTS

        public Player()
        {
            Money = 1000;
            Souls = new ushort[6] { 0, 0, 0, 0, 0, 0 };

            MaxLevelOfDevil = 1;
            NumberOfExorcisedDevils = 0;

            Damage = 1;
            AutoDamage = 0;
            DevilPower = 0;
        }

        #endregion

        #region ADD

        public void AddMoney(int value) => Money += value;
        public void AddSoul(byte index) => Souls[index] += 1;

        public void AddMaxLevelOfDevil() => MaxLevelOfDevil += 1;
        public void AddExorcisedDevil() => NumberOfExorcisedDevils += 1;

        public void AddDamage(int value) => Damage += value;
        public void AddAutoDamage(int value) => AutoDamage += value;

        #endregion

        #region REDUCE

        public void ReduceMoney(int value) => Money -= value;

        #endregion
    }
}