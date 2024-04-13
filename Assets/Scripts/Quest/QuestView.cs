using System;
using TMPro;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    [SerializeField] private TMP_Text _npcName;
    [SerializeField] private TMP_Text _questDescrption;
    [SerializeField] private TMP_Text _timerText;

    public event Action QuestComplete;
    public event Action QuestFailed;

    public void Init(Npc npc)
    {
        _npcName.text = npc.Name;
        SetQuestDescription(npc.ActiveQuest);
    }

    private void SetQuestDescription(Quest quest)
    {
        string questDescription = "";

        switch (quest.GetQuestItem().Type)
        {
            case ItemType.Collect:
                questDescription = "—обрать: " +
                                    quest.GetQuestItem().Name +
                                    " в количестве " +
                                    quest.GetItemNum().ToString();
                
                _questDescrption.text = questDescription;
                break;

            case ItemType.Destroy:
                questDescription = "”ничтожить: " +
                                    quest.GetQuestItem().Name +
                                    " в количестве " +
                                    quest.GetItemNum().ToString();

                _questDescrption.text = questDescription;
                break;
        }
    }
}
