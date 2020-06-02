using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thinktecture;

namespace Database
{
    public class RoleRepository
    {
        private readonly SampleDbContext _context;
        
        public RoleRepository(SampleDbContext context)
        {
            _context = context;
        }

        public async Task CreateRolesAsync(IEnumerable<RoleEntity> roles)
        {
            foreach (var role in roles)
            {
                if (!await _context.Roles.ContainsAsync(role).ConfigureAwait(false))
                {
                    await _context.Roles.AddAsync(role).ConfigureAwait(false);
                }
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<long?> GetRowNumAsync(Guid id, RoleSortField roleSortBy, bool ascending)
        {
            var query = _context.Roles;

            var rn = roleSortBy switch
            {
                RoleSortField.Name => ascending
                    ? query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderBy(x.Name)
                                .ThenBy(x.RoleId)
                        )
                    })
                    : query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderByDescending(x.Name)
                                .ThenByDescending(x.RoleId)
                        )
                    }),
                RoleSortField.Id => ascending
                    ? query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderBy(x.RoleId)
                                .ThenBy(x.RoleId)
                        )
                    })
                    : query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderByDescending(x.RoleId)
                                .ThenByDescending(x.RoleId)
                        )
                    }),
                RoleSortField.CreateDate => ascending
                    ? query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderBy(x.CreateDate)
                                .ThenBy(x.RoleId)
                        )
                    })
                    : query.Select(x => new
                    {
                        x.RoleId,
                        RowNumber = EF.Functions.RowNumber(
                            EF.Functions
                                .OrderByDescending(x.CreateDate)
                                .ThenByDescending(x.RoleId)
                        )
                    }),
                _ => throw new Exception()
            };

            var row = await rn
                .AsSubQuery()
                .SingleOrDefaultAsync(r => r.RoleId == id)
                .ConfigureAwait(false);

            return row?.RowNumber;
        }
    }
}