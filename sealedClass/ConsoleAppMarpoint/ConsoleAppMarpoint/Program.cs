
using System;

namespace ConsoleAppMarpoint
{
    public sealed class MyClass
    {
        public int NumOne { get; private set; }
        public int NumTwo { get; private set; }

        public MyClass(int _numOne, int numTwo)
        {
            NumOne = _numOne;
            NumTwo = numTwo;
        }

        int Add()
        {
            return NumOne + NumTwo;
        }
    }

    public static class MyClassExtensions
    {
        public static decimal Average(this MyClass value)
        {
            return (value.NumOne + value.NumTwo) / 2M;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myClass = new MyClass(50, 30);
            var avg = myClass.Average();

            Console.WriteLine(avg);
            Console.ReadLine();
        }
    }
}