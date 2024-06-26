using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveQuests", menuName = "Quest/ActiveQuests")]
public class ActiveQuests : ScriptableObject
{
    [SerializeField] private List<Quest> _data;

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

    public void ClearData(bool isClearData)
    {
        if (isClearData)
        {
            _data.Clear();
        }
    }

    public void AddQuest(Quest quest)
    {
        _data.Add(quest);
    }

    public void RemoveQuest(Quest quest)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            if (_data[i].NpcIndex == quest.NpcIndex)
            {
                _data.RemoveAt(i);
                return;
            }
        }
    }

    public void UpdateTimeLimit(float time, Quest quest)
    {
        for (int i = 0; i < _data.Count; i++)
        {
            if (_data[i].NpcIndex == quest.NpcIndex)
            {
                _data[i].UpdateTime(time);
                return;
            }
        }
    }
}
