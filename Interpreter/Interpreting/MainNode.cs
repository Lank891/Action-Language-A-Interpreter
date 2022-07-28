using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interpreter.Logic;
using Interpreter.Model;
using Interpreter.Model.Query;
using ExecutionContext = Interpreter.Logic.ExecutionContext;

namespace Interpreter.Interpreting
{
    public class MainNode
    {
        public LanguageDomainNode LanguageDomain { get; }
        public QueryBlockNode QueryBlock { get; }

        public MainNode(LanguageDomainNode languageDomain, QueryBlockNode queryBlock)
        {
            LanguageDomain = languageDomain;
            QueryBlock = queryBlock;
        }

        public LanguageDomain GetLanguageDomain()
        {
            return LanguageDomain.BuildLanguageDomain();
        }

        public IEnumerable<Query> GetQueries()
        {
            return QueryBlock.BuildQueryList();
        }
    }
}
