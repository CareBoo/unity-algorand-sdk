using System.Collections.Generic;

namespace AlgoSdk.Abi
{
    public interface IArgs<TEnumerator>
        where TEnumerator : IEnumerator<byte[]>
    {
        TEnumerator GetArgEnumerator(Method method);
    }
}
