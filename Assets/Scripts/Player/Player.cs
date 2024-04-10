using System;

public static class Player
{
    private static string _name = "�����";
    private static int _level = 5;
    private static float _moralLvl = 50;
    private static int _difficultyLvl = 2;

    public static float MoralLvl { get => _moralLvl; set => _moralLvl = value; }

    public static event Action OnPlayerStatsChanged;

    public static void IncreaseLevel()
    {
        _level++;
        OnPlayerStatsChanged?.Invoke();
    }

    public static void DecreaseLevel()
    {
        _level--;
        OnPlayerStatsChanged?.Invoke();
    }

    public static void IncreaseDiffLvl()
    {
        _difficultyLvl++;

        if (_difficultyLvl > 3)
        {
            _difficultyLvl = 1;
        }

        OnPlayerStatsChanged?.Invoke();
    }

    public static void DecreaseDiffLvl()
    {
        _difficultyLvl--;

        if ( _difficultyLvl < 1 )
        {
            _difficultyLvl = 3;
        }

        OnPlayerStatsChanged?.Invoke();
    }

    public static string GetPlayerInfo()
    {
        string playerInfo = "�����: " + _name + "\n" +
                            "�������: " + _level.ToString() + "\n" +
                            "�����: " + _moralLvl.ToString() + "\n" +
                            "������� ���������: " + _difficultyLvl.ToString();

        return playerInfo;
    }
}
