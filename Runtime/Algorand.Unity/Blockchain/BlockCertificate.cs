using System;

namespace Algorand.Unity
{
    [AlgoApiObject]
    public partial struct BlockCertificate
        : IEquatable<BlockCertificate>
    {
        public bool Equals(BlockCertificate other)
        {
            return true;
        }
    }
}
