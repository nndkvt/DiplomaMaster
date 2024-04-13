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

    public void CreateQuestView(Npc npc)
    {
        QuestView newQuestView = Instantiate(_questViewPrefab, _taskArea);

        newQuestView.Init(npc);
    }
}
