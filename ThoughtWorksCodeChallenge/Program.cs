using System;
using ThoughtWorksCodeChallenge.Model;
using ThoughtWorksCodeChallenge.BAL;
using System.Collections.Generic;

namespace ThoughtWorksCodeChallenge
{
    class Program
    {
        CreateOrder obj = new CreateOrder();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        public void CreateOrder()
        {
            obj.SaveOrder(1, new List<int> { 1, 2}, "55441");
        }
    }
}
