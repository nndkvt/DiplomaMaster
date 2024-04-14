using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveQuests", menuName = "Quest/ActiveQuests")]
public class ActiveQuests : ScriptableObject
{
    [SerializeField] private List<Quest> _data;
    [SerializeField] private bool _clearOnStart;

    public List<Quest> Data { get => _data; }

    public bool TryGetQuestByNpcIndex(int npcIndex, out Quest foundQuest)
    {
        foreach (Quest quest in _data)
        {
            if (quest.NpcIndex == npcIndex)
            {
                foundQuest = quest;
                return true;
            }
        }

        foundQuest = null;
        return false;
    }

    public void ClearData()
    {
        if (_clearOnStart)
        {
            _data.Clear();
        }
    }

    public void AddQuest(Quest quest)
    {
        _data.Add(quest);
    }
}
