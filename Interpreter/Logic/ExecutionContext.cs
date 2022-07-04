using Interpreter.Exceptions;
using Interpreter.Extensions;
using Interpreter.Model;
using Interpreter.Model.Domain;
using Interpreter.Model.Domain.Statement;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Interpreter.Model.Domain.Action;

namespace Interpreter.Logic
{
    public class ExecutionContext
    {
        private HashSet<State> AllPossibleStates { get; }
        private HashSet<State> InitialStates { get; } 
        private ImmutableHashSet<Action> Actions { get; }
        private ImmutableHashSet<AfterStatement> AfterStatements { get; }
        private ImmutableHashSet<CausesStatement> CausesStatements { get; }

        private ReadOnlyDictionary<State, ReadOnlyDictionary<Action, State>> StateTransitions { get; }

        public ExecutionContext(LanguageDomain domain)
        {
            AfterStatements = domain.AfterStatements.Copy().ToImmutableHashSet();
            CausesStatements = domain.CausesStatements.Copy().ToImmutableHashSet();

            Actions = domain.Actions.Select(name => new Action(name)).ToImmutableHashSet();

            AllPossibleStates = domain.GenerateAllInitialStates();
            StateTransitions = GetAllPossibleStateTransitions();

            InitialStates = AllPossibleStates.Copy();
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
                state = ApplyProgramToState(state, program);
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
                Fluent? fluentToBeChanged = state.FirstOrDefault(f => f.Name == fluent.Name);
                if (fluentToBeChanged == null)
                    state.Add(fluent);
                else
                    state.UpdateElement(fluentToBeChanged, f => { f.State = fluent.State; });
            }
        }

        private State ApplyProgramToState(State state, IReadOnlyList<Action> program)
        {
            foreach(var action in program)
            {
                state = StateTransitions[state][action];
            }

            return state.Copy();
        }

        private void RemoveInvalidInitialStatesAccordingToAfterStatements()
        {
            List<State> statesToRemove = new();
            foreach(var statement in AfterStatements)
            {
                foreach(var initialState in InitialStates)
                {
                    State state = initialState.Copy();
                    state = ApplyProgramToState(state, statement.Actions);
                    if (!state.Contains(statement.Fluent))
                        statesToRemove.Add(initialState);
                }

                foreach (var state in statesToRemove)
                    InitialStates.Remove(state);
                statesToRemove.Clear();
            }
        }
    
        private ReadOnlyDictionary<State, ReadOnlyDictionary<Action, State>> GetAllPossibleStateTransitions()
        {
            Dictionary<State, ReadOnlyDictionary<Action, State>> transitions = new();
            foreach(var state in AllPossibleStates)
            {
                Dictionary<Action, State> singleStateTransitions = new();
                foreach (var action in Actions)
                {
                    State newState = state.Copy();
                    ApplyCausesStatementsToState(newState, action);
                    singleStateTransitions.Add(action, newState);
                }

                transitions.Add(state, new ReadOnlyDictionary<Action, State>(singleStateTransitions));
            }
            
            return new ReadOnlyDictionary<State, ReadOnlyDictionary<Action, State>>(transitions);
        }
        
    }
}
