using AlgoSdk.Experimental.Abi;
using UnityEngine.UIElements;

public partial class ArgField
{
    public sealed class Txn : ArgField
    {
        public override IAbiValue Value => null;

        public Txn(string label, TransactionReferenceType txnRefType)
        {
            style.flexDirection = FlexDirection.Row;
            Add(new Label(label));
            Add(new Label("[Previous Payment Transaction]"));
        }
    }
}
