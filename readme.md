Running the app should execute the db migration, insert some sample records and attempt to retrieve the row number of one of the records. 

The connection string can be changed in the `OnConfiguring` method of class `SampleDbContext`.

Attempting to retrieve the row number throws the exception:

> System.InvalidOperationException: The LINQ expression 'DbSet<RoleEntity>
    .Select(r => new { 
        RoleId = r.RoleId, 
        RowNumber = __Functions_0
            .RowNumber(__Functions_0
                .OrderBy(r.Name)
                .ThenBy(r.RoleId))
     })
    .AsSubQuery()
    .Where(e => e.RoleId == __id_1)' could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation explicitly by inserting a call to either AsEnumerable(), AsAsyncEnumerable(), ToList(), or ToListAsync(). See https://go.microsoft.com/fwlink/?linkid=2101038 for more information.