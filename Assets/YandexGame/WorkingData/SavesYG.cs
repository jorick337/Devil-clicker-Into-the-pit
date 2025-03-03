using Game.Classes;

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

        public byte LevelOfDevil = 1;
        public ushort NumberOfExorcisedDevils = 0;

        public int Damage = 1;
        public int AutoDamage = 0;

        public static SavesYG operator +(SavesYG savesYG, Player player)
        {
            savesYG.Money = player.Money;

            savesYG.LevelOfDevil = player.LevelOfDevil;
            savesYG.NumberOfExorcisedDevils = player.NumberOfExorcisedDevils;

            savesYG.Damage = player.Damage;
            savesYG.AutoDamage = player.AutoDamage;

            return savesYG;
        }
    }
}