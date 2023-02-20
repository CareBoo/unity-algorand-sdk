using System.Collections.Generic;
using Algorand.Unity.Experimental.Abi;
using UnityEngine.UIElements;

namespace Algorand.Unity.Samples.CallingSmartContractAbi
{
    public partial class ArgField
    {
        public sealed class Tuple : ArgField
        {
            private readonly ListView list;
            private readonly List<ArgField> elements;

            public override IAbiValue Value => throw new System.NotImplementedException();

            public Tuple(string label, TupleType type)
            {
            }
        }
    }
}