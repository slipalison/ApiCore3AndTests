using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiCore3AndTests.Infra.Datas.Contexts
{
    public class TestContextDesingContext : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=TestBase;User Id=sa;Password=FkcS7GDBbK;");

            return new TestContext(optionsBuilder.Options);
        }
    }
}