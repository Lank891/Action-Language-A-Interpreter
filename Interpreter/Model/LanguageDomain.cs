using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model
{
    public class LanguageDomain
    {
        public const uint maximalNumberOfFluentsInDomain = 20; 

        public HashSet<string> Fluents { get; }
        public HashSet<string> Actions { get; }
        public HashSet<AfterStatement> AfterStatements { get; }
        public HashSet<CausesStatement> CausesStatements { get; }

        public LanguageDomain()
        {
            Fluents = new();
            Actions = new();
            AfterStatements = new();
            CausesStatements = new();
        }

        public void AddAfterStatement(AfterStatement afterStatement)
        {
            Fluents.Add(afterStatement.Fluent.Name);
            foreach (var action in afterStatement.Actions)
            {
                Actions.Add(action.Name);
            }
            AfterStatements.Add(afterStatement);
        }

        public void AddCausesStatement(CausesStatement causesStatement)
        {
            Fluents.Add(causesStatement.Fluent.Name);
            Actions.Add(causesStatement.Action.Name);
            foreach(var fluent in causesStatement.Condition)
            {
                Fluents.Add(fluent.Name);
            }
            CausesStatements.Add(causesStatement);
        }

        public override string ToString()
        {
            List<string> initiallyStatements = AfterStatements
                .Where(statement => !statement.Actions.Any())
                .Select(statement => statement.ToString())
                .OrderBy(str => str.Length)
                .ToList();
            List<string> afterStatements = AfterStatements
                .Where(statement => statement.Actions.Any())
                .Select(statement => statement.ToString())
                .OrderBy(str => str.Length)
                .ToList();
            List<string> causesStatements = CausesStatements
                .Select(statement => statement.ToString())
                .OrderBy(str => str.Length)
                .ToList();

            string ret = "";
            foreach (var statement in initiallyStatements)
                ret += statement + "\n";
            foreach (var statement in afterStatements)
                ret += statement + "\n";
            foreach (var statement in causesStatements)
                ret += statement + "\n";

            return ret[..^1];
        }

        public HashSet<HashSet<Fluent>> GenerateAllInitialStates()
        {
            if (Fluents.Count > maximalNumberOfFluentsInDomain)
                throw new ArgumentOutOfRangeException(nameof(Fluents), $"There are more than {maximalNumberOfFluentsInDomain} in the domain.");

            List<string> orderedFluents = Fluents.ToList();

            HashSet<HashSet<Fluent>> initialStates = new();

            for(uint i = 0; i < (1 << Fluents.Count); i++)
            {
                string combination = Convert.ToString(i, 2);
                while (combination.Length < Fluents.Count)
                    combination = "0" + combination;

                HashSet<Fluent> initialState = new();
                for(int f = 0; f < combination.Length; f++)
                {
                    initialState.Add(new Fluent(orderedFluents[f], combination[f] == '1'));
                }

                initialStates.Add(initialState);
            }

            return initialStates;
        }
    }
}
