﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TemplatePattern
{
    public class Sourdough : Bread
    {
        public override void MixIngredients()
        {
            Console.WriteLine("Gathering Ingredients for Sourdough Breed.");
        }

        public override void Bake()
        {
            Console.WriteLine("Baking the Sourdough Bread. (20 minutes)");
        }
    }
}
