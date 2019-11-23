using System;

namespace Facade
{
    public class Program
    {
        public static void Main()
        {
            var car = new CarBuilderFacade()
                .Info
                .WithType("BMW")
                .WithColor("Black")
                .WithNumberOfDoors(5)
                .Built
                .InCity("Leipzig")
                .AtAddress("Some address 54")
                .Build();

            Console.WriteLine(car);
        }
    }
}
