using System;
using Algorand.Unity.Algod;

namespace Algorand.Unity.Experimental.Abi
{
    public struct MethodCallResult
    {
        private static readonly byte[] ReturnHash = new byte[] { 0x15, 0x1f, 0x7c, 0x75 };
        public string TxnId { get; set; }

        public byte[] RawValue { get; set; }

        public IAbiValue ReturnValue { get; set; }

        public string DecodeError { get; set; }

        public PendingTransactionResponse TxnInfo { get; set; }

        public Method Method { get; set; }

        public MethodCallResult(
            string txnId,
            PendingTransactionResponse txnInfo,
            Method method
        )
        {
            TxnId = txnId;
            Method = method;
            TxnInfo = txnInfo;
            (DecodeError, RawValue, ReturnValue) = ParseResult(txnInfo, method);
        }

        private static (string, byte[], IAbiValue) ParseResult(
            PendingTransactionResponse txnInfo,
            Method method
            )
        {
            if (method.Returns.Type == null)
            {
                return (null, null, null);
            }

            if (txnInfo.Logs == null || txnInfo.Logs.Length == 0)
            {
                return ("app call transaction did not log a return value", null, null);
            }

            var result = txnInfo.Logs[txnInfo.Logs.Length - 1];
            if (!CheckReturnHash(result))
            {
                return ("app call transaction did not log a return value", null, null);
            }

            var rawValue = new byte[result.Length - ReturnHash.Length];
            Array.Copy(result, ReturnHash.Length, rawValue, 0, rawValue.Length);

            try
            {
                var (decodeError, returnValue) = method.Returns.Type.Decode(rawValue);
                return (decodeError, rawValue, returnValue);
            }
            catch (Exception ex)
            {
                return (ex.Message, rawValue, null);
            }
        }

        private static bool CheckReturnHash(byte[] result)
        {
            if (result == null || result.Length < ReturnHash.Length)
            {
                return false;
            }

            for (var i = 0; i < ReturnHash.Length; i++)
            {
                if (result[i] != ReturnHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
