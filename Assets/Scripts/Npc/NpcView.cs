using TMPro;
using UnityEngine;

public class NpcView : MonoBehaviour
{
    [SerializeField] TMP_Text _nameText;

    private Npc _npc;

    public void Init(Npc npc)
    {
        _npc = npc;

        _nameText.text = _npc.Name;
    }
}
