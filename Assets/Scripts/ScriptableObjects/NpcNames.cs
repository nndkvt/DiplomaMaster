using UnityEngine;

[CreateAssetMenu(fileName = "NpcNamesData", menuName = "Npc/NpcNamesData")]
public class NpcNames : ScriptableObject
{
    [SerializeField] private string[] _names;

    public string[] Names => _names;

    public string GetRandomName()
    {
        System.Random rnd = new System.Random();

        return _names[rnd.Next(0, _names.Length)];
    }
}
