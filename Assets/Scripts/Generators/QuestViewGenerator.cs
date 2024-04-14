using UnityEngine;

public class QuestViewGenerator : MonoBehaviour
{
    [SerializeField] private Transform _taskArea;
    [SerializeField] private QuestView _questViewPrefab;

    public static QuestViewGenerator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CreateQuestView(NpcView npcView)
    {
        QuestView newQuestView = Instantiate(_questViewPrefab, _taskArea);

        newQuestView.QuestComplete += npcView.AttachedQuestEnded;
        newQuestView.QuestFailed += npcView.AttachedQuestEnded;

        newQuestView.Init(npcView.GetNpc());
    }
}
