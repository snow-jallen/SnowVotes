using Microsoft.EntityFrameworkCore;

public class VoteContext : DbContext
{
    public VoteContext(DbContextOptions<VoteContext> dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Vote> Votes { get; set; }


}

public class Vote
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Total { get; set; }
}