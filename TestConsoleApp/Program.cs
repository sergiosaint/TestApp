using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestConsoleApp
{
    class Program
    {
        static DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
        static TestDbContext dbContext = new TestDbContext(options);

        static Stopwatch watch;
        static void Main(string[] args)
        {
            dbContext.Seconds.Add(new SecondsContainer(1));
            dbContext.Seconds.Add(new SecondsContainer(2));
            dbContext.Seconds.Add(new SecondsContainer(1));
            dbContext.Seconds.Add(new SecondsContainer(1));
            dbContext.Seconds.Add(new SecondsContainer(1));
            dbContext.SaveChanges();


            Console.WriteLine("start async");
            watch = Stopwatch.StartNew();
            RunAsync().Wait();
            watch.Stop();
            Console.WriteLine("Elapsed Milliseconds async: " + watch.ElapsedMilliseconds);

            Console.WriteLine("start parallel");
            watch.Restart();
            RunParallel();
            watch.Stop();
            Console.WriteLine("Elapsed Milliseconds parallel: " + watch.ElapsedMilliseconds);

            Console.ReadKey();
        }


        static async Task RunAsync() {
            await dbContext.Seconds.ForEachAsync( m => { Thread.Sleep(m.Seconds * 1000); } );
        }

        static void RunParallel()
        {
            Parallel.ForEach(dbContext.Seconds, m => { Thread.Sleep(m.Seconds * 1000); });
        }
    }
}
