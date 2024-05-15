namespace Proto.App.Models;

public class Metadata
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }

    public int Height { get; set; }
    public Metadata Left { get; set; }
    public Metadata Right { get; set; }


    public string Serialize()
    {
        return $"Id: {Id}, Name: {Name}, Size: {Size}, Created: {Created}, Modified: {Modified}, Height: {Height}";
    }
}