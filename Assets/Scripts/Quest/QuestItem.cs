using System;

public enum ItemType
{
    Collect = 0,
    Destroy = 1,
}

[Serializable]
public class QuestItem
{
    public string Name;
    public ItemType Type;
    public int Level;

    public QuestItem(string name, ItemType type, int level)
    {
        Name = name;
        Type = type;
        Level = level;
    }
}
