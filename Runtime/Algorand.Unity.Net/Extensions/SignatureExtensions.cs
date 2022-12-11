using System.Linq;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.Net
{
    public static class SignatureExtensions
    {
        public static Signature ToDotnet(this Sig from)
        {
            return from == default ? null : new Signature(from.ToArray());
        }

        public static Sig ToUnity(this Signature from)
        {
            if (from == null)
            {
                return default;
            }
            var bytes = from.Bytes;
            var sig = default(Sig);
            sig.CopyFrom(bytes, 0);
            return sig;
        }

        public static MultisigSignature ToDotnet(this MultisigSig from)
        {
            return new MultisigSignature
            {
                Version = from.Version,
                Threshold = from.Threshold,
                Subsigs = from.Subsigs.Select(ToDotnet).ToList()
            };
        }

        public static MultisigSig ToUnity(this MultisigSignature from)
        {
            return new MultisigSig
            {
                Subsigs = from.Subsigs.Select(ToUnity).ToArray(),
                Threshold = (byte)from.Threshold,
                Version = (byte)from.Version
            };
        }

        public static MultisigSubsig ToDotnet(this MultisigSig.Subsig from)
        {
            return new MultisigSubsig(from.PublicKey.ToArray(), from.Sig.ToArray());
        }

        public static MultisigSig.Subsig ToUnity(this MultisigSubsig from)
        {
            var pk = new Ed25519.PublicKey();
            var subsigKeyBytes = from.key.GetEncoded();
            pk.CopyFrom(subsigKeyBytes, 0);
            return new MultisigSig.Subsig
            {
                PublicKey = pk,
                Sig = from.sig.ToUnity()
            };
        }

        public static LogicsigSignature ToDotnet(this LogicSig from)
        {
            return new Algorand.LogicsigSignature(
                from.Program,
                from.Args.Select(a => a.ToArray()).ToList(),
                from.Sig == default ? null : from.Sig.ToArray(),
                from.Multisig.ToDotnet()
            );
        }

        public static LogicSig ToUnity(this LogicsigSignature from)
        {
            var args = new FixedList128Bytes<byte>[from.Args.Count];
            for (var i = 0; i < args.Length; i++)
            {
                args[i] = new FixedList128Bytes<byte>();
                for (var j = 0; j < from.Args[i].Length; j++)
                {
                    args[i][j] = from.Args[i][j];
                }
            }

            return new LogicSig
            {
                Program = from.Logic,
                Args = args,
                Multisig = from.Msig.ToUnity(),
                Sig = from.Sig.ToUnity()
            };
        }
    }
}
