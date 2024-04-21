using System;

[System.Serializable]
public class Npc
{
    [UnityEngine.SerializeField] private int _id;
    [UnityEngine.SerializeField] private string _name;
    [UnityEngine.SerializeField] private bool[] _character;
    [UnityEngine.SerializeField] private Quest _activeQuest;

    public int Id { get => _id; }
    public string Name { get => _name; }
    public bool[] Character { get => _character; }
    public Quest ActiveQuest { get => _activeQuest; }

    public event Action<float> OnActiveQuestComplete;

    public Npc(int id, string name, bool[] character)
    {
        _id = id;
        _name = name;
        _character = character;
        _activeQuest = null;

        OnActiveQuestComplete += DetatchQuest;
    }

    ~Npc()
    {
        OnActiveQuestComplete -= DetatchQuest;
    }

    public void QuestCompleted(float deltaRel = 0)
    {
        OnActiveQuestComplete?.Invoke(deltaRel);
    }

    public void AssignQuest(Quest newQuest)
    {
        _activeQuest = newQuest;
    }

    private void DetatchQuest()
    {
        _activeQuest = null;
    }

    private void DetatchQuest(float num)
    {
        DetatchQuest();
    }
}
