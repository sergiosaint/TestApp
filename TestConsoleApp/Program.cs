using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

            var list = new List<SecondsContainer>() { new SecondsContainer(1), new SecondsContainer(2), new SecondsContainer(1), new SecondsContainer(1), new SecondsContainer(2) };

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

            Console.WriteLine("start select and wait");
            watch.Restart();
            var tasks = list.Select(async t => await RunAsync(t));
            Task.WhenAll(tasks).Wait();
            watch.Stop();
            Console.WriteLine("Elapsed Milliseconds select and wait: " + watch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        static async Task RunAsync(SecondsContainer container)
        {
            Console.WriteLine($"waiting" + DateTime.UtcNow.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));
            await Delay(container.Seconds);
        }

        static async Task Delay(int seconds)
        {
            Console.WriteLine($"waiting {seconds}seconds");
            await Task.Delay(seconds * 1000);
        }

        static async Task RunAsync() {
            await dbContext.Seconds.ForEachAsync( m => {
                Console.WriteLine($"waiting {m.Seconds}seconds");
                Thread.Sleep(m.Seconds * 1000); } );
        }

        static void RunParallel()
        {
            Parallel.ForEach(dbContext.Seconds, m => {
                Console.WriteLine($"waiting {m.Seconds}seconds");
                Thread.Sleep(m.Seconds * 1000); });
        }
    }
}
