using System;

namespace SingletonDemo
{
    class Program
    {
        static void Main()
        {
            var db = SingletonDataContainer.Instance;
            var db2 = SingletonDataContainer.Instance;
            var db3 = SingletonDataContainer.Instance;
            var db4 = SingletonDataContainer.Instance;
            var db5 = SingletonDataContainer.Instance;
        }
    }
}
