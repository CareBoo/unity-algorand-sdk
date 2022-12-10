using Algorand.Algod;

namespace Algorand.Unity.Net
{
    public static class AlgodClientExtensions
    {
        /// <summary>
        /// Convert <see cref="AlgodClient"/> to the Algorand2 <see cref="CommonApi"/>.
        /// </summary>
        /// <param name="algod">The Unity Serializable <see cref="AlgodClient"/>.</param>
        /// <returns>An Algorand2 <see cref="CommonApi"/></returns>
        public static CommonApi ToCommonApi(this AlgodClient algod)
        {
            return new CommonApi(algod.ToHttpClient());
        }

        /// <summary>
        /// Convert <see cref="AlgodClient"/> to the Algorand2 <see cref="DefaultApi"/>.
        /// </summary>
        /// <param name="algod">The Unity Serializable <see cref="AlgodClient"/>.</param>
        /// <returns>An Algorand2 <see cref="DefaultApi"/></returns>
        public static DefaultApi ToDefaultApi(this AlgodClient algod)
        {
            return new DefaultApi(algod.ToHttpClient());
        }
    }
}
