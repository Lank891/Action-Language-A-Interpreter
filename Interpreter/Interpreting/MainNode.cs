using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
