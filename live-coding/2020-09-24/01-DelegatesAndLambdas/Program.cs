using System;

namespace DelegatesAndLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            MathOp f = Add;
            f = Subtract;
            Console.WriteLine(f(84, 42));

            CalculateAndPrint(21, 21, Add);

            CalculateAndPrint(21, 21, (x, y) => x * y);
            CalculateAndPrint(21, 21, (x, y) => x + y);
            CalculateAndPrint(21, 21, (x, y) => x - y);

            CalculateAndPrint("A", "B", (x, y) => x + y);
            CalculateAndPrint(true, true, (x, y) => x && y);
        }

        delegate T Combine<T>(T a, T b);

        delegate int MathOp(int x, int y);

        static void CalculateAndPrint<T>(T x, T y, Combine<T> f)
        {
            var result = f(x, y);
            Console.WriteLine(result);
        }

        static int Add(int x, int y)
        {
            return x + y;
        }

        static int Subtract(int a, int b)
        {
            return a - b;
        }
    }
}
