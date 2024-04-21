using UnityEngine;
using Random = System.Random;

public class NpcGenerator : MonoBehaviour
{
    [SerializeField] private NpcNames _namesData;
    [SerializeField] private Transform _npcHolder;
    [SerializeField] private NpcView _npcPrefab;

    private CreatedNpcs NpcData => DataHolder.Instance.NpcData;

    private int _npcCount;

    private void Awake()
    {
        // ѕроверка на существование данных персонажей
        if (NpcData.Data.Count != 0)
        {
            LoadCreatedNpcs();
        }
        else
        {
            GenerateNewNpcs();
        }
    }

    private void LoadCreatedNpcs()
    {
        foreach (Npc npc in NpcData.Data)
        {
            NpcView newNpcView = Instantiate(_npcPrefab, _npcHolder);

            newNpcView.Init(npc);
        }

        RelationshipDataManipulator.LoadRelationshipData();
        RelationshipDataManipulator.DebugLogRelationships();
    }

    private void GenerateNewNpcs()
    {
        Random random = new Random();

        _npcCount = random.Next(10, 16);

        for (int i = 0; i < _npcCount; i++)
        {
            NpcView newNpcView = Instantiate(_npcPrefab, _npcHolder);

            string npcName = _namesData.GetRandomName();

            bool[] character = GenerateRandomCharacter();

            Npc newNpc = new Npc(i + 1, npcName, character);

            NpcData.AddNpc(newNpc);

            newNpcView.Init(newNpc);
        }

        RelationshipDataManipulator.GenerateInitialRelationshipValues(_npcCount);
    }

    private bool[] GenerateRandomCharacter()
    {
        bool[] character = new bool[4];

        Random random = new Random();

        for (int i = 0; i < 4; i++)
        {
            bool characterValue = random.NextDouble() > 0.5 ? true : false;

            character[i] = characterValue;
        }

        return character;
    }
}
