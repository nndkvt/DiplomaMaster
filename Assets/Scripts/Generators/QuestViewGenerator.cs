using UnityEngine;

public class QuestViewGenerator : MonoBehaviour
{
    [SerializeField] private Transform _taskArea;
    [SerializeField] private QuestView _questViewPrefab;

    private ActiveQuests ActiveQuests => DataHolder.Instance.ActiveQuests;

    public static QuestViewGenerator Instance;

    private void Awake()
    {
        Instance = this;

        foreach (Quest quest in ActiveQuests.Data)
        {
            CreateQuestView(quest);
        }
    }

    public void CreateQuestView(Quest quest)
    {
        QuestView newQuestView = Instantiate(_questViewPrefab, _taskArea);
        newQuestView.Init(quest);
    }
}
