using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public class StandardEF : Attempt
    {
        public override string Name => "Standard Entity Framework";
        public override string NameInitials => "SEF";
        public override void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent)
        {
            context.Sons.AddRange(sons);
            context.Parents.AddRange(parents);
            context.SaveChanges();
        }
    }
}
