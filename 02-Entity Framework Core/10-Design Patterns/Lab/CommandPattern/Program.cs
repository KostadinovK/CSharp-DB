﻿using System;
using CommandPattern.Contracts;
using CommandPattern.Enums;

namespace CommandPattern
{
    public class Program
    {
        public static void Main()
        {
            var modifyPrice = new ModifyPrice();
            var product = new Product("Phone", 500);

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 200));

            Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100));

            Console.WriteLine(product);
        }

        private static void Execute(Product product, ModifyPrice modifyPrice, ICommand productCommand)
        {
            modifyPrice.SetCommand(productCommand);
            modifyPrice.Invoke();
        }
    }
}
