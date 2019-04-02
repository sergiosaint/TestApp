using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using TestEFInserts.Entities;

namespace TestEFInserts.Attempts
{
    public class BulkExtensionsPlain : Attempt
    {
        public override string Name => "Bulk Extensions Plain";
        public override string NameInitials => "BEP";
        public override void Run(TestDbContext context, IList<Parent> parents, IList<Son> sons, IList<SonWithAutoParent> sonsWithAutoParent)
        {
            Console.WriteLine($"{Name} Problem: Sons don't have the parent id set");

            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsert(parents, new BulkConfig { SetOutputIdentity = true });
                context.BulkInsert(sons);
                transaction.Commit();
            }
        }
    }
}
