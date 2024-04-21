using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestAcceptWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _declineButton;

    private ActiveQuests ActiveQuests => DataHolder.Instance.ActiveQuests;
    private ActiveQuests GeneratedQuests => DataHolder.Instance.GeneratedQuests;

    private Quest _attachedQuest;

    public static QuestAcceptWindow Instance;

    public void Init(Quest quest)
    {
        gameObject.SetActive(true);

        _attachedQuest = quest;

        _descriptionText.text = GenerateQuestDescription();
    }

    private void Awake()
    {
        Instance = this;

        _acceptButton.onClick.AddListener(OnAcceptButtonPressed);
        _declineButton.onClick.AddListener(OnDeclineButtonPressed);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _acceptButton.onClick.RemoveAllListeners();
        _declineButton.onClick.RemoveAllListeners();
    }

    private void OnAcceptButtonPressed()
    {
        GeneratedQuests.RemoveQuest(_attachedQuest);
        ActiveQuests.AddQuest(_attachedQuest);

        QuestViewGenerator.Instance.CreateQuestView(_attachedQuest);

        gameObject.SetActive(false);
    }

    private void OnDeclineButtonPressed()
    {
        RelationshipDataManipulator.RecalculateRelationships(_attachedQuest.NpcIndex, _attachedQuest.DecreaseRel);

        Npc npc = DataHolder.Instance.NpcData.GetNpcByIndex(_attachedQuest.NpcIndex);

        npc.QuestCompleted(_attachedQuest.DecreaseRel);

        gameObject.SetActive(false);
    }

    private string GenerateQuestDescription()
    {
        string description = "";

        string npcName = DataHolder.Instance.NpcData.GetNpcByIndex(_attachedQuest.NpcIndex).Name;

        description += $"{npcName} просит об услуге:\n";

        switch (_attachedQuest.Item.Type)
        {
            case ItemType.Collect:
                description += $"Собрать: {_attachedQuest.Item.Name} в количестве {_attachedQuest.ItemNum}.\n\n";
                break;

            case ItemType.Destroy:
                description += $"Уничтожить: {_attachedQuest.Item.Name} в количестве {_attachedQuest.ItemNum}.\n\n";
                break;
        }

        if (_attachedQuest.TimeLimit > 0)
        {
            float timeLimit = _attachedQuest.TimeLimit;

            if ((timeLimit % 60).ToString().Length == 1)
            {
                description += $"Ограничение по времени: {timeLimit / 60}:0{timeLimit % 60}";
            }
            else
            {
                description += $"Ограничение по времени: {timeLimit / 60}:{timeLimit % 60}";
            }
        }

        return description;
    }
}
