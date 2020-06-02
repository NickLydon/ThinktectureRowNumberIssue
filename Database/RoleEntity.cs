using System;

namespace Database
{
    public class RoleEntity
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
    }
}