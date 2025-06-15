using static Bogus.DataSets.Name;

namespace _3_AnonimObject;

public class User
//public class User
{
    public int Id { get; private set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public string Avatar { get; set; } = String.Empty;
    public Gender Gender { get; set; }

    public User(int id)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"{Id}\t{LastName} {FirstName}\t{Email}\t{UserName}\t{Avatar}";
    }
}
