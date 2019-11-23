using System;
using System.Collections.Generic;
using System.Text;
using SingletonDemo.Contracts;

namespace SingletonDemo
{
    public class SingletonDataContainer : ISingletonContainer
    {
        private Dictionary<string, int> _capitals;

        private static SingletonDataContainer instance;

        public static SingletonDataContainer Instance
        {
            get
            { 
                 if(instance == null)
                 {
                    instance = new SingletonDataContainer();
                 }

                 return instance;
            }
        
        }

        private SingletonDataContainer()
        {
            Console.WriteLine("Initializing singleton object");

            _capitals = new Dictionary<string, int>();
        }

        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }
}
