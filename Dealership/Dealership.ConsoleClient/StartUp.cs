namespace Dealership.ConsoleClient
{
    using System;
    using Core;

    public class StartUp
    {
        public static void Main()
        {
            Engine.Create(Console.Out).Start();
        }
    }
}