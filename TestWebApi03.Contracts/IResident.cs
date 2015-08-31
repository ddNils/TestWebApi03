namespace TestWebApi03.Contracts
{
    public interface IResident
    {
        int Id { get; set; }
        string JobDescription { get; set; }
        string LastName { get; set; }
        string Name { get; set; }
        string Title { get; set; }
    }
}