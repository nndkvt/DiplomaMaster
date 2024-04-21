using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatToChange
{
    Level = 0,
    Difficulty = 1,
}

[RequireComponent(typeof(Button))]
public class PlayerStatsButton : MonoBehaviour
{
    [SerializeField] private PlayerStatToChange _stat;
    [SerializeField] private bool _changeDirection;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangePlayerStat);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangePlayerStat);
    }

    public void ChangePlayerStat()
    {
        switch (_stat)
        {
            case PlayerStatToChange.Level:
                if (_changeDirection)
                {
                    Player.IncreaseLevel();
                }
                else
                {
                    Player.DecreaseLevel();
                }
                break;

            case PlayerStatToChange.Difficulty:
                if (_changeDirection)
                {
                    Player.IncreaseDiffLvl();
                }
                else
                {
                    Player.DecreaseDiffLvl();
                }
                break;
        }
    }
}
