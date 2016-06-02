using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.ServiceConfigurators;

namespace VOIPService
{
    public interface IBuilder<T> where T : class
    {
        ServiceConfigurator<T> Build();
    }
}
