namespace Algorand.Unity.Net
{
    public static class CompiledTealExtensions
    {
        public static TEALProgram ToDotnet(this CompiledTeal from)
        {
            return new TEALProgram(from.Bytes);
        }

        public static CompiledTeal ToUnity(this TEALProgram from)
        {
            return from.Bytes;
        }
    }
}
