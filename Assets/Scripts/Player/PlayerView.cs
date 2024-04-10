using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class PlayerView : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        SetText();

        Player.OnPlayerStatsChanged += SetText;
    }

    private void OnDisable()
    {
        Player.OnPlayerStatsChanged -= SetText;
    }

    private void SetText()
    {
        _text.text = Player.GetPlayerInfo();
    }
}
