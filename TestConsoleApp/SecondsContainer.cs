using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsoleApp
{
    public class SecondsContainer
    {
        public int Id { get; set; }
        public int Seconds { get; set; }

        public SecondsContainer()
        {
        }

        public SecondsContainer(int secs)
        {
            Seconds = secs;
        }
    }
}
