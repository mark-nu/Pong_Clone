namespace Assets.Scripts.GameConfig
{
    public enum GameMode { ONE_PLAYER, TWO_PLAYER };
    public enum GameDifficulty { EASY = 5, MEDIUM = 10, HARD = 20 };
    public enum GameLength { SHORT = 11, LONG = 21 };

    public interface ISelectedGame
    {
        GameMode GameMode { get; set; }
        GameDifficulty GameDifficulty { get; set; }
        GameLength GameLength { get; set; }
    }

    public class SelectedGameData : ISelectedGame
    {
        public GameMode GameMode { get; set; }
        public GameDifficulty GameDifficulty { get; set; }
        public GameLength GameLength { get; set; }
    }
}