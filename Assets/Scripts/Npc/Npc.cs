public class Npc
{
    private int _id;
    private string _name;
    private bool[] _character;

    public int Id { get => _id; }
    public string Name { get => _name; }
    public bool[] Character { get => _character; }

    public Npc(int id, string name, bool[] character)
    {
        _id = id;
        _name = name;
        _character = character;
    }
}
