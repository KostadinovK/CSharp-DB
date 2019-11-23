using System;
using System.Collections.Generic;
using System.Text;

namespace PrototypePattern
{
    public class Sandwich : SandwichPrototype
    {
        private string bread;
        private string meat;
        private string cheese;
        private string veggies;

        public Sandwich(string bread, string meat, string cheese, string veggies)
        {
            this.bread = bread;
            this.cheese = cheese;
            this.meat = meat;
            this.veggies = veggies;
        }

        public override SandwichPrototype Clone()
        {
            var ingredients = GetIngredientsList();
            Console.WriteLine("Cloning sandwich with ingredients: {0}", ingredients);

            return MemberwiseClone() as SandwichPrototype;
        }

        private string GetIngredientsList()
        {
            return $"{bread}, {meat}, {cheese}, {veggies}";
        }
    }
}
