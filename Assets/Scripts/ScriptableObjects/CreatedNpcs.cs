using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NpcData", menuName = "Npc/NpcData")]
public class CreatedNpcs : ScriptableObject
{
    [SerializeField] private List<Npc> _data;
    [SerializeField] private bool _clearOnStart;

    public List<Npc> Data { get => _data; }

    public void ClearData()
    {
        if (_clearOnStart)
        {
            _data.Clear();
        }
    }

    public void AddNpc(Npc npc)
    {
        _data.Add(npc);
    }

    public Npc GetNpcByIndex(int npcIndex)
    {
        foreach (var npc in _data)
        {
            if (npcIndex == npc.Id)
            {
                return npc;
            }
        }

        return null;
    }
}
