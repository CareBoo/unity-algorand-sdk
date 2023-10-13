namespace Algorand.Unity.Crypto
{
    public partial struct PwHash
    {
        public enum OpsLimit : ulong
        {
            None = 0,
            Interactive = 2,
            Max = 4294967295,
            Min = 2,
            Moderate = 3,
            Sensitive = 4
        }

        public enum MemLimit : ulong
        {
            None = 0,
            Interactive = 67108864,
            Max = 4398046510080,
            Min = 8192,
            Moderate = 134217728,
            Sensitive = 1073741824
        }
    }
}
