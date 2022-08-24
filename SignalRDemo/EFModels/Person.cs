using System;
using System.Collections.Generic;

namespace SignalRDemo.EFModels
{
    public partial class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
