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
    private float _timeTillQuest;

    private Random random = new Random();

    public void Init(Npc npc)
    {
        _npc = npc;

        _nameText.text = _npc.Name;

        _timeTillQuest = random.Next(0, _maxQuestWaitTime);

        UpdateTimer();

        StartCoroutine(QuestTimer());

        _takeQuestButton.onClick.AddListener(OnQuestButtonPressed);
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

    private IEnumerator QuestTimer()
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
        StopCoroutine(QuestTimer());

        // —юда логику добавлени€ нового квеста

        _takeQuestButton.gameObject.SetActive(false);

        _timerText.gameObject.SetActive(true);
    }
}
