using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace api_aquaguard_dotnet.Data
{
    public class AquaGuardContextFactory : IDesignTimeDbContextFactory<AquaGuardContext>
    {
        public AquaGuardContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AquaGuardContext>();

            // Use a mesma connection string que você já colocou no Program.cs
            optionsBuilder.UseOracle(
                "User Id=rm554983;Password=191205;" +
                "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))" +
                "(CONNECT_DATA=(SERVICE_NAME=ORCL)))"
            );

            return new AquaGuardContext(optionsBuilder.Options);
        }
    }
}
