public class Quest
{
    private QuestItem _item;
    private int _itemNum;
    private float _timeLimit;
    private float _increaseRel;
    private float _decreaseRel;

    public Quest(QuestItem item, int itemNum, float timeLimit, float increaseRel, float decreaseRel)
    {
        _item = item;
        _itemNum = itemNum;
        _timeLimit = timeLimit;
        _increaseRel = increaseRel;
        _decreaseRel = decreaseRel;
    }
}
