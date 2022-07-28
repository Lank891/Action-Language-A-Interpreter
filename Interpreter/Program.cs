using Interpreter.Interpreting;
using Interpreter.Logic;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using Interpreter.Model.Query;
using ExecutionContext = Interpreter.Logic.ExecutionContext;

namespace Interpreter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new AInterpreter();
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
            MainNode mainNode = parser.Parse(str);
            
            LanguageDomain languageDomain = mainNode.GetLanguageDomain();
            ExecutionContext executionContext = new(languageDomain);
            QueryExecutor queryExecutor = new(executionContext);
            
            IEnumerable<Query> queries = mainNode.GetQueries();

            Console.WriteLine(languageDomain);
            Console.WriteLine();
            foreach(Query query in queries)
            {
                bool result = query.Execute(queryExecutor);

                Console.WriteLine($"{query}: {(result ? "YES" : "NO")}");
            }
        }
    }
}