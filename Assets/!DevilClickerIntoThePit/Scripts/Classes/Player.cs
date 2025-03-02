namespace Game.Classes
{
    [System.Serializable]
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