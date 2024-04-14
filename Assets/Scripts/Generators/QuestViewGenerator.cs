using UnityEngine;

public class QuestViewGenerator : MonoBehaviour
{
    [SerializeField] private Transform _taskArea;
    [SerializeField] private QuestView _questViewPrefab;

    private CreatedNpcs NpcData => DataHolder.Instance.NpcData;
    private ActiveQuests ActiveQuests => DataHolder.Instance.ActiveQuests;

    public static QuestViewGenerator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        foreach (Quest quest in ActiveQuests.Data)
        {
            Npc npc = NpcData.GetNpcByIndex(quest.NpcIndex);

            CreateQuestView(npc);
        }
    }

    public void CreateQuestView(Npc npc)
    {
        QuestView newQuestView = Instantiate(_questViewPrefab, _taskArea);
        newQuestView.Init(npc);
    }
}
