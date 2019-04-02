using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TestEFInserts.Attempts;
using TestEFInserts.Entities;

namespace TestEFInserts
{
    class Program
    {
        static TestDbContext context;

        static List<Parent> parents;
        static List<Son> sons;
        static List<SonWithAutoParent> sonsWithAutoParent;
        static List<Toy> toys;

        static void Main(string[] args)
        {

            var watch = Stopwatch.StartNew();

            List<Attempt> attempts = new List<Attempt> {
                new StandardEF(),
                //new StandardEFAutoParents(),
                //new BulkExtensionsPlain(),
                new BulkExtensionsSetIdsManually(),
                //new BulkExtensionsSetIdsAutomatically(),
                new StandardEF(),
                //new StandardEFAutoParents(),
                //new BulkExtensionsPlain(),
                new BulkExtensionsSetIdsManually(),
                //new BulkExtensionsSetIdsAutomatically(),
                new StandardEF(),
                //new StandardEFAutoParents(),
                //new BulkExtensionsPlain(),
                new BulkExtensionsSetIdsManually(),
                //new BulkExtensionsSetIdsAutomatically()
            };

            
            foreach (var attempt in attempts) {
                Setup(attempt.NameInitials);
                watch.Restart();
                attempt.Run(context, parents, sons, sonsWithAutoParent);
                watch.Stop();
                Console.WriteLine($"{attempt.Name} took {watch.ElapsedMilliseconds}ms");
            }


            Console.ReadKey();
        }

        static void Setup( string name ) {
            context = new TestDbContext();

            parents = new List<Parent>();
            sons = new List<Son>();
            sonsWithAutoParent = new List<SonWithAutoParent>();
            toys = new List<Toy>();

            for (int i = 0; i < 120; i++)
            {
                var parent = new Parent() { Name = "P" + i + name };
                parents.Add(parent);

                for (int j = 0; j < 55; j++)
                {
                    var son = new Son() { Parent = parent, Name = "S" + j + name };
                    var sonsToys = new List<Toy>();
                    for (int l = 0; l < 10; l++)
                    {
                        var toy = new Toy() { Name = "S" + j + "T" + l + name };
                        sonsToys.Add(toy);
                    }
                    son.Toys = sonsToys;
                    sons.Add(son);

                    var sonWithAutoParent = new SonWithAutoParent() { Parent = parent, Name = "S" + j + name };
                    sonsWithAutoParent.Add(sonWithAutoParent);
                }
            }
        }
    }
}
