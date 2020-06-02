using System;
using System.Threading.Tasks;
using Database;
using Microsoft.EntityFrameworkCore;

namespace EntryPoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var context = new SampleDbContext();
            await context.Database.MigrateAsync().ConfigureAwait(false);

            var repo = new RoleRepository(context);
            await repo.CreateRolesAsync(Roles).ConfigureAwait(false);
            var row1 = await repo.GetRowNumAsync(Roles[0].RoleId, RoleSortField.Name, true).ConfigureAwait(false);
        }

        private static readonly RoleEntity[] Roles = new[]
        {
            new RoleEntity()
            {
                Name = "A",
                RoleId = Guid.Parse("0A4C4861-1E8E-4A85-8BD8-37322C2519A0"),
                CreateDate = new DateTime(2010, 1, 1)
            }, 
            new RoleEntity()
            {
                Name = "B",
                RoleId = Guid.Parse("C25D5E26-B95A-4C59-8CC9-3F5DA5A07F04"),
                CreateDate = new DateTime(2015, 1, 1)
            },
            new RoleEntity()
            {
                Name = "C",
                RoleId = Guid.Parse("0C1A7025-D81B-480F-8594-6CEA6C3DC192"),
                CreateDate = new DateTime(2020, 1, 1)
            }, 
        };
    }
}