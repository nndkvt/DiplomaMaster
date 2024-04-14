[System.Serializable]
public class Quest
{
    [UnityEngine.SerializeField] private QuestItem _item;
    [UnityEngine.SerializeField] private int _itemNum;
    [UnityEngine.SerializeField] private float _timeLimit;
    [UnityEngine.SerializeField] private float _increaseRel;
    [UnityEngine.SerializeField] private float _decreaseRel;
    [UnityEngine.SerializeField] private int _npcIndex;

    public QuestItem Item { get => _item; }
    public int ItemNum { get => _itemNum; }
    public float TimeLimit { get => _timeLimit; }
    public float IncreaseRel { get => _increaseRel; }
    public float DecreaseRel { get => _decreaseRel; }
    public int NpcIndex { get => _npcIndex; }

    public Quest(QuestItem item, int itemNum, float timeLimit, float increaseRel, float decreaseRel, int npcIndex)
    {
        _item = item;
        _itemNum = itemNum;
        _timeLimit = timeLimit;
        _increaseRel = increaseRel;
        _decreaseRel = decreaseRel;
        _npcIndex = npcIndex;
    }
}
