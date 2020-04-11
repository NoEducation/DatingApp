using System;
using System.Collections.Generic;
using System.Text;

namespace Dating.Tests
{
    public class WebHostFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
    }
}
