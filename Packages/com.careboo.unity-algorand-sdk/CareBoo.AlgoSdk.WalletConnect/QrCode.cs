using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace AlgoSdk.WalletConnect
{
    public static class QrCode
    {
        /// <summary>
        /// Generates a 256x256 QR-Code
        /// </summary>
        /// <param name="text">The text to use to generate the QR-Code</param>
        /// <returns>A <see cref="Texture2D"/> with width=256 and height=256.</returns>
        public static Texture2D Generate(string text)
        {
            var encoded = new Texture2D(256, 256);
            var color32 = Encode(text, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            return encoded;
        }

        static Color32[] Encode(string textForEncoding, int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }
    }
}
