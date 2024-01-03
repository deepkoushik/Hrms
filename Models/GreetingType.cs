using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class GreetingType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Greeting> Greetings { get; } = new List<Greeting>();
}
