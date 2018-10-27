using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTestApi.Models
{
    public class MovieItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long StartAt { get; set; }
        public bool IsComplete { get; set; }
    }
}