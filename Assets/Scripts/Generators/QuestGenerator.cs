using UnityEngine;
using Random = System.Random;
using MLModel = IncreaseRelationshipModelML.Model;

public class QuestGenerator : MonoBehaviour
{
    [SerializeField] private QuestItems _questItemsData;

    [SerializeField] private int _minQuestItemsNum;

    [SerializeField] private int _maxQuestItemsNum;

    [SerializeField] private float _minTimeLimitInMinutes;

    private Random random = new Random();

    public static QuestGenerator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Quest GenerateQuest(Npc npc)
    {
        int maxItemLvl = Player.Level + Player.DifficultyLevel;

        QuestItem questItem = _questItemsData.GetRandomItem();

        while (questItem.Level > maxItemLvl)
        {
            questItem = _questItemsData.GetRandomItem();
        }

        int maxItemNum = Player.Level * Player.DifficultyLevel / questItem.Level;

        if (maxItemNum > _maxQuestItemsNum)
        {
            maxItemNum = _maxQuestItemsNum;
        }

        int itemNum = random.Next(_minQuestItemsNum, maxItemNum);

        float timeProb = (float)Player.DifficultyLevel / (float)(3 * 3);

        float timeLimit = -1;

        if (random.NextDouble() < timeProb)
        {
            timeLimit = _minTimeLimitInMinutes * 60 * (itemNum / (questItem.Level * Player.DifficultyLevel));

            if (timeLimit < _minTimeLimitInMinutes * 60)
            {
                timeLimit = _minTimeLimitInMinutes * 60;
            }
        }

        /*
        MLModel.ModelInput modelInput = new MLModel.ModelInput(npc.Character, 75, questItem.Level);

        float increaseRelValue = MLModel.ConsumeModel.Predict(modelInput).Score;

        float decreaseRelValue = increaseRelValue / 2;

        return new Quest(questItem, itemNum, timeLimit, increaseRelValue, decreaseRelValue);
        */

        // Testing
        return new Quest(questItem, itemNum, timeLimit, 5, -5, npc.Id);
    }
}
