using Random = System.Random;
using UnityEngine;
using DeltaMLModel = DeltaRelationshipModelML.Model;

public static class RelationshipDataManipulator
{
    public static float[,] relationshipData;

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

                    Debug.Log($"Rel between {i} and {j} is {relationshipData[i, j]}");
                }
                else
                {
                    relationshipData[i, j] = r.Next(30, 96);
                    relationshipData[j, i] = relationshipData[i, j];

                    Debug.Log($"Rel between {i} and {j} is {relationshipData[i, j]}");
                }
            }
        }

        for (int j = 0; j <= npcNum; j++)
        {
            relationshipData[0, j] = 50 + Player.MoralLvl / 2;
            relationshipData[j, 0] = relationshipData[0, j];

            Debug.Log($"Rel between Player and {j} is {relationshipData[0, j]}");
        }
    }

    public static void RecalculateRelationships(int npcIndex, float deltaValue)
    {

    }
}
