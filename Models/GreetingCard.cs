using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class GreetingCard
{
    public int Id { get; set; }

    public byte[]? Card { get; set; }

    public virtual ICollection<Greeting> Greetings { get; } = new List<Greeting>();
}
