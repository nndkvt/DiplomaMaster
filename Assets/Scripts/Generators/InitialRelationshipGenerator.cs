using UnityEngine;
using Random = System.Random;

public static class InitialRelationshipGenerator
{
    public static void GenerateInitialRelationshipValues(int npcNum)
    {
        Random r = new Random();

        float[,] relationshipMatrix = new float[npcNum+1,npcNum+1];

        for (int i = 1; i <= npcNum; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                if (i == j)
                {
                    relationshipMatrix[i,j] = 0;

                    Debug.Log($"Rel between {i} and {j} is {relationshipMatrix[i, j]}");
                }
                else
                {
                    relationshipMatrix[i, j] = r.Next(30, 96);
                    relationshipMatrix[j, i] = relationshipMatrix[i, j];

                    Debug.Log($"Rel between {i} and {j} is {relationshipMatrix[i,j]}");
                }
            }
        }

        for (int j = 0; j <= npcNum; j++)
        {
            relationshipMatrix[0, j] = 50 + Player.MoralLvl / 2;
            relationshipMatrix[j, 0] = relationshipMatrix[0, j];

            Debug.Log($"Rel between Player and {j} is {relationshipMatrix[0, j]}");
        }
    }
}
