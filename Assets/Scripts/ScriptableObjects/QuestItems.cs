using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "QuestItems", menuName = "Quest/QuestItems")]
public class QuestItems : ScriptableObject
{
    [SerializeField] private QuestItem[] _items;

    public QuestItem[] Items { get => _items; }

    public QuestItem GetRandomItem()
    {
        Random rnd = new Random();

        return _items[rnd.Next(0, _items.Length)];
    }
}
