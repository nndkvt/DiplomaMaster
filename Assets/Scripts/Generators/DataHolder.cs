using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public CreatedNpcs NpcData;
    public ActiveQuests ActiveQuests;
    public ActiveQuests GeneratedQuests;
    public Dictionary<NpcView, QuestView> AttachedQuests;

    [SerializeField] private bool _clearDataOnStart;

    public static DataHolder Instance;

    private void Awake()
    {
        Instance = this;

        NpcData.ClearData(_clearDataOnStart);
        ActiveQuests.ClearData(_clearDataOnStart);
        GeneratedQuests.ClearData(_clearDataOnStart);
    }
}
