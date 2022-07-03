using Interpreter.Exceptions;
using Interpreter.Extensions;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;
using State = System.Collections.Generic.HashSet<Interpreter.Model.Domain.Fluent>;

namespace Interpreter.Logic
{
    public class ExecutionContext
    {
        private HashSet<State> InitialStates { get; } 
        private ImmutableHashSet<AfterStatement> AfterStatements { get; }
        private ImmutableHashSet<CausesStatement> CausesStatements { get; }

        public ExecutionContext(LanguageDomain domain)
        {
            AfterStatements = domain.AfterStatements.Copy().ToImmutableHashSet();
            CausesStatements = domain.CausesStatements.Copy().ToImmutableHashSet();
            InitialStates = domain.GenerateAllInitialStates();

            RemoveInvalidInitialStatesAccordingToAfterStatements();

            if (InitialStates.Count == 0)
                throw new InconsistentDomainException("There are no initial states satysfying initially and after statements.");
        }

        public IEnumerable<(State initial, State final)> GetResultsOfProgramExecution(IReadOnlyList<Action> program)
        {
            List<(State initial, State final)> results = new();
            foreach(var initialState in InitialStates)
            {
                State state = initialState.Copy();
                ApplyProgramToState(state, program);
                results.Add((initialState.Copy(), state));
            }
            return results;
        }

        private void ApplyCausesStatementsToState(State state, Action action)
        {
            State fluentsToChange = new();
            foreach(var causes in CausesStatements)
            {
                if (causes.Action != action)
                    continue;

                if (!causes.Condition.IsProperSubsetOf(state))
                    continue;

                if (fluentsToChange.Any(fluent => fluent.Name == causes.Fluent.Name && fluent.State != causes.Fluent.State))
                    throw new InconsistentDomainException(action, state, causes.Fluent.Name);

                fluentsToChange.Add(causes.Fluent.Copy());
            }

            foreach(var fluent in fluentsToChange)
            {
                Fluent? fluentToChange = state.FirstOrDefault(f => f.Name == fluent.Name);
                if (fluentToChange != null)
                    fluentToChange.State = fluent.State;
                else
                    state.Add(fluent.Copy());
            }
        }

        private void ApplyProgramToState(State state, IReadOnlyList<Action> program)
        {
            foreach(var action in program)
            {
                ApplyCausesStatementsToState(state, action);
            }
        }

        private void RemoveInvalidInitialStatesAccordingToAfterStatements()
        {
            List<State> statesToRemove = new();
            foreach(var statement in AfterStatements)
            {
                foreach(var initialState in InitialStates)
                {
                    State state = initialState.Copy();
                    ApplyProgramToState(state, statement.Actions);
                    if (!state.Any(fluent => fluent == statement.Fluent))
                        statesToRemove.Add(initialState);
                }

                foreach (var state in statesToRemove)
                    InitialStates.Remove(state);
                statesToRemove.Clear();
            }
        }
    }
}
