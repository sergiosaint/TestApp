using EFCore.BulkExtensions;
using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public class BulkExtensionsSetIdsManually : Attempt
    {
        public override string Name => "Bulk Extensions setting ids manually";
        public override string NameInitials => "BEM";
        public override void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                //PreserveInsertOrder stetting set to true so that it doesnt create a new parent when setting the id
                context.BulkInsert(parents, new BulkConfig { SetOutputIdentity = true, PreserveInsertOrder = true });
                foreach (var son in sons)
                {
                    son.ParentId = son.Parent.ParentId;
                }
                context.BulkInsert(sons, new BulkConfig { SetOutputIdentity = true, PreserveInsertOrder = true });

                var allToys = new List<Toy>();
                foreach (var son in sons)
                {
                    foreach (var toy in son.Toys)
                    {
                        toy.SonId  = son.SonId;
                    }
                    allToys.AddRange(son.Toys);
                }

                context.BulkInsert(allToys);
                transaction.Commit();
            }
        }
    }
}
