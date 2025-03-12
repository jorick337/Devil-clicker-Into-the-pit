using Game.Classes.Player;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Мое:
        public int Money = 0;
        public ushort[] Souls = new ushort[6] { 0, 0, 0, 0, 0, 0 };

        public byte MaxLevelOfDevil = 1;
        public byte MaxLevelOfPit = 0;
        public ushort NumberOfExorcisedDevils = 0;

        public int Damage = 1;
        public int AutoDamage = 0;
        public int DevilPower = 0;

        public static SavesYG operator +(SavesYG savesYG, Player player)
        {
            savesYG.Money = player.Money;
            savesYG.Souls = player.Souls;

            savesYG.MaxLevelOfDevil = player.MaxLevelOfDevil;
            savesYG.MaxLevelOfPit = player.MaxLevelOfPit;
            savesYG.NumberOfExorcisedDevils = player.NumberOfExorcisedDevils;

            savesYG.Damage = player.Damage;
            savesYG.AutoDamage = player.AutoDamage;
            savesYG.DevilPower = player.DevilPower;

            return savesYG;
        }
    }
}