using System;

namespace Algorand.Unity
{
    [Serializable]
    public struct GenesisAccountAlloc
    {
        public string addr;
        public string comment;
        public State state;

        [Serializable]
        public struct State
        {
            public ulong algo;
            public ulong onl;
            public string sel;
            public string vote;
            public ulong voteKD;
            public ulong voteLst;
        }
    }

    /// <summary>
    /// JSON information from <see cref="IAlgodClient.GetGenesisInformation"/>
    /// </summary>
    [Serializable]
    public struct GenesisInformation
    {
        /// <summary>
        /// Genesis account allocations
        /// </summary>
        public GenesisAccountAlloc[] alloc;
        public string fees;
        public string id;
        public string network;
        public string proto;
        public string rwd;
    }
}
