using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public abstract class Attempt
    {
        public virtual string Name { get; set; }
        public virtual string NameInitials { get; set; }
        public abstract void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent);
    }
}
