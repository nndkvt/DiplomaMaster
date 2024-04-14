using System.Collections;
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

    private Random random = new Random();

    public void Init(Npc npc)
    {
        _npc = npc;

        _nameText.text = _npc.Name;

        _takeQuestButton.onClick.AddListener(OnQuestButtonPressed);

        if (_npc.ActiveQuest == null && 
            !ActiveQuests.TryGetQuestByNpcIndex(_npc.Id, out Quest quest))
        {
            ActivateQuestTimer();
        }
        else
        {
            ActiveQuests.TryGetQuestByNpcIndex(_npc.Id, out Quest newQuest);
            _npc.AssignQuest(newQuest);
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
    }

    private void ActivateQuestTimer()
    {
        _timeTillQuest = random.Next(0, _maxQuestWaitTime);

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

        ActiveQuests.AddQuest(_npc.ActiveQuest);

        QuestViewGenerator.Instance.CreateQuestView(this);

        _takeQuestButton.gameObject.SetActive(false);

        _timerText.gameObject.SetActive(true);
    }

    public Npc GetNpc()
    {
        return _npc;
    }

    public void AttachedQuestEnded(float deltaRel = 0)
    {
        _npc.DetatchQuest();

        Debug.Log($"Quest Complete for npc {_npc.Name}");

        ActivateQuestTimer();
    }
}
