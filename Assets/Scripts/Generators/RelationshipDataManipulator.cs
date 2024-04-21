using Random = System.Random;
using UnityEngine;
using DeltaML = DeltaRelationshipModelML.Model;
using InitialML = InitialRelationshipModelML.Model;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public static class RelationshipDataManipulator
{
    private static float[,] relationshipData;

    private static string relDataJsonPath = "Assets/Json/RelData.json";

    public static void LoadRelationshipData()
    {
        string jsonText = File.ReadAllText(relDataJsonPath);

        relationshipData = JsonConvert.DeserializeObject<float[,]>(jsonText);
    }

    public static void GenerateInitialRelationshipValues(int npcNum)
    {
        Random r = new Random();

        relationshipData = new float[npcNum + 1, npcNum + 1];

        for (int i = 1; i <= npcNum; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                if (i == j)
                {
                    relationshipData[i, j] = 0;
                }
                else
                {
                    relationshipData[i, j] = r.Next(30, 96);
                    relationshipData[j, i] = relationshipData[i, j];
                }
            }
        }

        for (int j = 0; j <= npcNum; j++)
        {
            if (j == 0)
            {
                relationshipData[0, j] = 0;
                relationshipData[j, 0] = relationshipData[0, j];
                continue;
            }

            relationshipData[0, j] = 50 + Player.MoralLvl / 2;
            relationshipData[j, 0] = relationshipData[0, j];
        }

        DebugLogRelationships();

        string jsonText = JsonConvert.SerializeObject(relationshipData, Formatting.Indented);

        File.WriteAllText(relDataJsonPath, jsonText);
    }

    public static void RecalculateRelationships(int npcIndex, float deltaValue)
    {
        ChangeRelData(0, npcIndex, deltaValue);

        Random r = new Random();

        for (int i = 1; i < relationshipData.GetLength(0); i++)
        {
            if (i == npcIndex)
            {
                continue;
            }
            /*
            Npc npc = DataHolder.Instance.NpcData.GetNpcByIndex(i);

            float relNpc12 = relationshipData[i, npcIndex];
            
            DeltaML.ModelInput inputData = new DeltaML.ModelInput(npc.Character, relNpc12, deltaValue);

            DeltaML.ModelOutput result = DeltaML.ConsumeModel.Predict(inputData);
            
            ChangeRelData(i, npcIndex, result.Score);
            */
            float deltaNew = (float)r.NextDouble() * deltaValue;

            ChangeRelData(0, i, deltaNew);
        }

        DebugLogRelationships();

        string jsonText = JsonConvert.SerializeObject(relationshipData, Formatting.Indented);

        File.WriteAllText(relDataJsonPath, jsonText);
    }

    public static void DebugLogRelationships()
    {
        string debugString = "";

        for (int i = 0; i < relationshipData.GetLength(0); i++)
        {
            for (int j = 0; j < relationshipData.GetLength(1); j++)
            {
                debugString += relationshipData[i, j] + ", ";
                //Debug.Log($"Relationship between {i} and {j}: {relationshipData[i, j]}");
            }
            debugString += "\n";
        }

        Debug.Log(debugString);
    }

    private static void ChangeRelData(int index1, int index2, float delta)
    {
        relationshipData[index1, index2] += delta;

        if (relationshipData[index1, index2] > 100)
        {
            relationshipData[index1, index2] = 100;
        }
        else if (relationshipData[index1, index2] < 0)
        {
            relationshipData[index1, index2] = 0;
        }

        relationshipData[index2, index1] = relationshipData[index1, index2];
    }
}
