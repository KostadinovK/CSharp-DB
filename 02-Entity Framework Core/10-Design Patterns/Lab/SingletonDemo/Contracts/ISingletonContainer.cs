using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonDemo.Contracts
{
    public interface ISingletonContainer
    {
        int GetPopulation(string name);
    }
}
