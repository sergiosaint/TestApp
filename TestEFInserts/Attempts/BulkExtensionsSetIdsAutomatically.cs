using EFCore.BulkExtensions;
using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public class BulkExtensionsSetIdsAutomatically : Attempt
    {
        public override string Name => "Bulk Extensions setting ids automatically";
        public override string NameInitials => "BEA";
        public override void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                //PreserveInsertOrder stetting set to true so that it doesnt create a new parent when setting the id
                context.BulkInsert(parents, new BulkConfig { SetOutputIdentity = true, PreserveInsertOrder = true });
                context.BulkInsert(sonsWithAutoParent);
                transaction.Commit();
            }
        }
    }
}
