using System;
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

    public event Action<float> QuestComplete;
    public event Action<float> QuestFailed;

    private int _timerTime;

    private Quest _quest;

    public void Init(Npc npc)
    {
        _npcName.text = npc.Name;
        _quest = npc.ActiveQuest;

        SetQuestDescription(_quest);

        _completeButton.onClick.AddListener(OnCompleteButtonPressed);

        if (npc.ActiveQuest.TimeLimit > 0)
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
                questDescription = "—обрать: " +
                                    quest.Item.Name +
                                    " в количестве " +
                                    quest.ItemNum.ToString();
                
                _questDescrption.text = questDescription;
                break;

            case ItemType.Destroy:
                questDescription = "”ничтожить: " +
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
        }

        QuestFailed?.Invoke(_quest.DecreaseRel);
        Destroy(gameObject);
    }

    private void UpdateTimer()
    {
        int minutes = _timerTime / 60;
        int seconds = _timerTime % 60;

        _timerText.text = minutes.ToString() + ":" +
                            seconds.ToString();
    }

    private void OnCompleteButtonPressed()
    {
        QuestComplete?.Invoke(_quest.IncreaseRel);
        Destroy(gameObject);
    }

    private void OnMinusSecondsButtonPressed()
    {
        _timerTime -= 10;
        UpdateTimer();
    }
}
