using UnityEngine;
using Random = System.Random;

public class NpcGenerator : MonoBehaviour
{
    [SerializeField] private NpcNames _namesData;
    [SerializeField] private Transform _npcHolder;
    [SerializeField] private NpcView _npcPrefab;

    private int _npcCount;

    private void Awake()
    {
        Random random = new Random();

        _npcCount = random.Next(10, 16);

        for (int i = 0; i < _npcCount; i++)
        {
            var newNpcView = Instantiate(_npcPrefab, _npcHolder);

            string npcName = _namesData.GetRandomName();

            bool[] character = GenerateRandomCharacter();

            Npc newNpc = new Npc(i + 1, npcName, character);

            newNpcView.Init(newNpc);
        }

        InitialRelationshipGenerator.GenerateInitialRelationshipValues(_npcCount);
    }

    private bool[] GenerateRandomCharacter()
    {
        bool[] character = new bool[4];

        Random random = new Random();

        for (int i =0; i < 4; i++)
        {
            bool characterValue = random.NextDouble() > 0.5 ? true : false;

            character[i] = characterValue;
        }

        return character;
    }
}
