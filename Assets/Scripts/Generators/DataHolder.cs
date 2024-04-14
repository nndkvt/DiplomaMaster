using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public CreatedNpcs NpcData;
    public ActiveQuests ActiveQuests;
    public Dictionary<NpcView, QuestView> AttachedQuests;

    public static DataHolder Instance;

    private void Awake()
    {
        Instance = this;

        NpcData.ClearData();
        ActiveQuests.ClearData();
    }
}
