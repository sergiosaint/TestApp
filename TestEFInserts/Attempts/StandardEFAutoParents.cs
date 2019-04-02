using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public class StandardEFAutoParents : Attempt
    {
        public override string Name => "Standard Entity Framework with auto parents";
        public override string NameInitials => "SEFA";
        public override void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent)
        {
            context.Parents.AddRange(parents);
            context.SonWithAutoParent.AddRange(sonsWithAutoParent);
            context.SaveChanges();
        }
    }
}
