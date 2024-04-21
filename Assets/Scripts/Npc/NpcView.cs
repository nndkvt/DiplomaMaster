using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class NpcView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _takeQuestButton;
    [SerializeField] private int _maxQuestWaitTime;

    private Npc _npc;
    private float _timeTillQuest = 0;

    private ActiveQuests ActiveQuests => DataHolder.Instance.ActiveQuests;
    private ActiveQuests GeneratedQuests => DataHolder.Instance.GeneratedQuests;

    private Random random = new Random();

    public void Init(Npc npc)
    {
        _npc = npc;

        _npc.OnActiveQuestComplete += AttachedQuestEnded;

        _nameText.text = _npc.Name;

        _takeQuestButton.onClick.AddListener(OnQuestButtonPressed);

        if (ActiveQuests.TryGetQuestByNpcIndex(_npc.Id, out Quest quest))
        {
            _npc.AssignQuest(quest);
        }
        else if (GeneratedQuests.TryGetQuestByNpcIndex(_npc.Id, out Quest quest1))
        {
            _npc.AssignQuest(quest1);
            _takeQuestButton.gameObject.SetActive(true);
        }
        else
        {
            ActivateQuestTimer();
        }
    }

    private void OnDisable()
    {
        _takeQuestButton.onClick.RemoveAllListeners();
    }

    private void QuestTimeIsUp()
    {
        _takeQuestButton.gameObject.SetActive(true);

        _timerText.gameObject.SetActive(false);

        Quest newQuest = QuestGenerator.Instance.GenerateQuest(_npc);

        _npc.AssignQuest(newQuest);

        GeneratedQuests.AddQuest(newQuest);
    }

    private void ActivateQuestTimer()
    {
        _timerText.gameObject.SetActive(true);

        _timeTillQuest = random.Next(5, _maxQuestWaitTime);

        UpdateTimer();

        StartCoroutine(StartQuestTimer());
    }

    private IEnumerator StartQuestTimer()
    {
        while (_timeTillQuest > 0)
        {
            yield return new WaitForSeconds(1);

            _timeTillQuest--;
            UpdateTimer();
        }

        QuestTimeIsUp();
    }

    private void UpdateTimer()
    {
        _timerText.text = _timeTillQuest.ToString();
    }

    private void OnQuestButtonPressed()
    {
        StopCoroutine(StartQuestTimer());

        QuestAcceptWindow.Instance.Init(_npc.ActiveQuest);

        _takeQuestButton.gameObject.SetActive(false);
    }

    private void AttachedQuestEnded(float deltaRel)
    {
        Debug.Log($"Quest Complete for npc {_npc.Name}, relationship changed by {deltaRel}");

        ActivateQuestTimer();
    }
}
