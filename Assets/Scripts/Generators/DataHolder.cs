using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public CreatedNpcs NpcData;
    public ActiveQuests ActiveQuests;

    public static DataHolder Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        NpcData.ClearData();
        ActiveQuests.ClearData();
    }
}
