using Melozing;

public class GameSettings : ManualSingletonMono<GameSettings>
{

    public enum Difficulty { Easy, Medium, Hard }
    public Difficulty SelectedDifficulty { get; private set; }

    public void SetDifficulty(Difficulty difficulty)
    {
        SelectedDifficulty = difficulty;
    }
}
