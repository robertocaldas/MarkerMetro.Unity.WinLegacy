﻿using System;

#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#elif WINDOWS_PHONE
using System.Security.Cryptography;
using System.Text;
#endif


namespace MarkerMetro.Unity.WinLegacy.Security.Cryptography
{
    public static class EncryptionProvider
    {

        /// <summary>
        /// Encrypt a string using dual encryption method. Returns an encrypted text.
        /// </summary>
        /// <param name="toEncrypt">String to be encrypted</param>
        /// <param name="key">Unique key for encryption/decryption</param>m>
        /// <returns>Returns encrypted string.</returns>
        public static string Encrypt(string toEncrypt, string key)
        {
#if NETFX_CORE
            try
            {

                // Get the MD5 key hash (you can as well use the binary of the key string)
                var keyHash = HashProvider.GetMD5Hash(key);

                // Create a buffer that contains the encoded message to be encrypted.
                var toDecryptBuffer = CryptographicBuffer.ConvertStringToBinary(toEncrypt, BinaryStringEncoding.Utf8);

                // Open a symmetric algorithm provider for the specified algorithm.
                var aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

                // Create a symmetric key.
                var symetricKey = aes.CreateSymmetricKey(keyHash);

                // The input key must be securely shared between the sender of the cryptic message
                // and the recipient. The initialization vector must also be shared but does not
                // need to be shared in a secure manner. If the sender encodes a message string
                // to a buffer, the binary encoding method must also be shared with the recipient.
                var buffEncrypted = CryptographicEngine.Encrypt(symetricKey, toDecryptBuffer, null);

                // Convert the encrypted buffer to a string (for display).
                // We are using Base64 to convert bytes to string since you might get unmatched characters
                // in the encrypted buffer that we cannot convert to string with UTF8.
                var strEncrypted = CryptographicBuffer.EncodeToBase64String(buffEncrypted);

                return strEncrypted;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("[EncryptionProvider] Error Encrypting a string");
            }
#else
            throw new System.PlatformNotSupportedException();
#endif
        }

        /// <summary>
        /// Decrypt a string using dual encryption method. Return a Decrypted clear string
        /// </summary>
        /// <param name="cipherString">Encrypted string</param>
        /// <param name="key">Unique key for encryption/decryption</param>
        /// <returns>Returns decrypted text.</returns>
        public static string Decrypt(string cipherString, string key)
        {
#if NETFX_CORE
            try
            {
                // Get the MD5 key hash (you can as well use the binary of the key string)
                var keyHash = HashProvider.GetMD5Hash(key);

                // Create a buffer that contains the encoded message to be decrypted.
                IBuffer toDecryptBuffer = CryptographicBuffer.DecodeFromBase64String(cipherString);

                // Open a symmetric algorithm provider for the specified algorithm.
                SymmetricKeyAlgorithmProvider aes = SymmetricKeyAlgorithmProvider.OpenAlgorithm(SymmetricAlgorithmNames.AesEcbPkcs7);

                // Create a symmetric key.
                var symetricKey = aes.CreateSymmetricKey(keyHash);

                var buffDecrypted = CryptographicEngine.Decrypt(symetricKey, toDecryptBuffer, null);

                string strDecrypted = CryptographicBuffer.ConvertBinaryToString(BinaryStringEncoding.Utf8, buffDecrypted);

                return strDecrypted;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("[EncryptionProvider] Error Decrypting a string");
            }
#else
            throw new System.PlatformNotSupportedException();
#endif
        }
    }

}
