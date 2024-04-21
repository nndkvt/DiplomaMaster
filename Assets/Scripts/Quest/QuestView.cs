using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private TMP_Text _npcName;
    [SerializeField] private TMP_Text _questDescrption;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _completeButton;
    [SerializeField] private Button _minusSecondsButton;

    private ActiveQuests ActiveQuests => DataHolder.Instance.ActiveQuests;

    private int _timerTime;

    private Quest _quest;

    public void Init(Quest quest)
    {
        _npcName.text = DataHolder.Instance.NpcData.GetNpcByIndex(quest.NpcIndex).Name;
        _quest = quest;

        SetQuestDescription(_quest);

        _completeButton.onClick.AddListener(OnCompleteButtonPressed);

        if (quest.TimeLimit > 0)
        {
            ActivateTimer(_quest);
            _minusSecondsButton.onClick.AddListener(OnMinusSecondsButtonPressed);
        }
    }

    private void OnDestroy()
    {
        _minusSecondsButton.onClick.RemoveAllListeners();
    }

    private void SetQuestDescription(Quest quest)
    {
        string questDescription = "";

        switch (quest.Item.Type)
        {
            case ItemType.Collect:
                questDescription = "Собрать: " +
                                    quest.Item.Name +
                                    " в количестве " +
                                    quest.ItemNum.ToString();
                
                _questDescrption.text = questDescription;
                break;

            case ItemType.Destroy:
                questDescription = "Уничтожить: " +
                                    quest.Item.Name +
                                    " в количестве " +
                                    quest.ItemNum.ToString();

                _questDescrption.text = questDescription;
                break;
        }
    }

    private void ActivateTimer(Quest quest)
    {
        _timerText.gameObject.SetActive(true);
        _minusSecondsButton.gameObject.SetActive(true);

        StartCoroutine(StartQuestTimer(quest.TimeLimit));
    }

    private IEnumerator StartQuestTimer(float initTime)
    {
        _timerTime = (int)initTime;

        UpdateTimer();

        while (_timerTime > 0)
        {
            yield return new WaitForSeconds(1);

            _timerTime--;
            UpdateTimer();

            ActiveQuests.UpdateTimeLimit(_timerTime, _quest);
        }

        // Расчет взаимоотношений и завершение квеста
        RelationshipDataManipulator.RecalculateRelationships(_quest.NpcIndex, _quest.DecreaseRel);

        Npc npc = DataHolder.Instance.NpcData.GetNpcByIndex(_quest.NpcIndex);
        npc.QuestCompleted(_quest.DecreaseRel);

        ActiveQuests.RemoveQuest(_quest);
        Destroy(gameObject);
    }

    private void UpdateTimer()
    {
        int minutes = _timerTime / 60;
        int seconds = _timerTime % 60;

        if (seconds.ToString().Length == 1)
        {
            _timerText.text = minutes.ToString() + ":0" +
                            seconds.ToString();
        }
        else
        {
            _timerText.text = minutes.ToString() + ":" +
                            seconds.ToString();
        }
    }

    private void OnCompleteButtonPressed()
    {
        // Расчет взаимоотношений и завершение квеста
        RelationshipDataManipulator.RecalculateRelationships(_quest.NpcIndex, _quest.IncreaseRel);

        Npc npc = DataHolder.Instance.NpcData.GetNpcByIndex(_quest.NpcIndex);
        npc.QuestCompleted(_quest.IncreaseRel);

        ActiveQuests.RemoveQuest(_quest);
        Destroy(gameObject);
    }

    private void OnMinusSecondsButtonPressed()
    {
        _timerTime -= 10;
        UpdateTimer();
    }
}
