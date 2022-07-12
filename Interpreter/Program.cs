using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;

namespace Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Interpreting.AInterpreter();
            const string str = @"
initially f;
initially ~g;
~f after A1, A2;
g after A2, A1;

A2 causes ~f;
A1 causes g if ~f;

is g after A1, A2?
is ~f after A2, A1?
";
            var res = parser.Parse(str);
            Console.WriteLine(res);
        }
    }
}