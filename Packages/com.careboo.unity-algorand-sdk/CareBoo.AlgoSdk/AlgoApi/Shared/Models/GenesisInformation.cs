using System;

namespace AlgoSdk
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

    [Serializable]
    public struct GenesisInformation
    {
        public GenesisAccountAlloc[] alloc;
        public string fees;
        public string id;
        public string network;
        public string proto;
        public string rwd;
    }
}
