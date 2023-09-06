namespace Algorand.Unity.WalletConnect.Core
{
    public enum EnvelopeType : byte
    {
        /// <summary>
        ///     <para>
        ///         Used when peers agreed on symmetric key and both are able to seal and open the sealbox.
        ///     </para>
        ///     <para>algo: ChaCha20-Poly1305</para>
        ///     <para>
        ///         structure:
        ///         <list type="bullet">
        ///             <item>tp - type byte (1 byte) = 0</item>
        ///             <item>iv - initialization vector (12 bytes)</item>
        ///             <item>ct - ciphertext (N bytes)</item>
        ///             <item>tag - authentication tag (16 bytes)</item>
        ///             <item>sb - sealbox: ct + tag</item>
        ///         </list>
        ///     </para>
        ///     <para>Serialized Envelope: <c>tp + iv + sb</c></para>
        ///     <para>https://docs.walletconnect.com/2.0/specs/clients/core/crypto/crypto-envelopes#type-0-envelope</para>
        /// </summary>
        Type0 = 0,

        /// <summary>
        ///     <para>
        ///         Used by client that is able to seal the message but it's peer is unable to open the sealbox as it is missing
        ///         public key for Diffie Hellman key agreement. After deriving symmetric key using pk and private key associated
        ///         with the topic the envelope has been received on both peers are able to seal and open the sealbox.
        ///     </para>
        ///     <para>algo: ChaCha20-Poly1305</para>
        ///     <para>
        ///         structure:
        ///         <list type="bullet">
        ///             <item>tp - type byte (1 byte) = 1</item>
        ///             <item>pk - public key (32 bytes)</item>
        ///             <item>iv - initialization vector (12 bytes)</item>
        ///             <item>ct - ciphertext (N bytes)</item>
        ///             <item>tag - authentication tag (16 bytes)</item>
        ///             <item>sb - sealbox: ct + tag</item>
        ///         </list>
        ///     </para>
        ///     <para>Serialized Type 1 Envelope: <c>tp + pk + iv + sb</c></para>
        ///     <para>https://docs.walletconnect.com/2.0/specs/clients/core/crypto/crypto-envelopes#type-1-envelope</para>
        /// </summary>
        Type1 = 1
    }
}