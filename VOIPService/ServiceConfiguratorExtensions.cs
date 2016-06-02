using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf.ServiceConfigurators;

namespace VOIPService
{
    public static class ServiceConfiguratorExtensions
    {
        public static ServiceConfigurator<SIPService> ConstructUsing(this ServiceConfigurator<SIPService> self, Action<SIPServiceConfigurator> configure)
        {
            var configurator = new SIPServiceConfigurator();
            configure(configurator);
            self.ConstructUsing((a) => configurator.Build());
            return self;
        }


        //public static ServiceConfigurator<T> ConstructUsing<T, TC>(this ServiceConfigurator<T> self, Action<TC> configure) 
        //    where T : class
        //    where TC : IBuilder<T>, new()
        //{
        //    var configurator = new TC();
        //    configure(configurator);

        //    var x = configurator.Build();

        //    self.ConstructUsing((y) => x);

        //    return self;
        //}

    }
}
