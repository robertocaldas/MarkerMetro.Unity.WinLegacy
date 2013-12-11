﻿#if NETFX_CORE
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#endif

using System;

namespace MarkerMetro.Unity.WinLegacy.Cryptography
{
    public class SHA1 : IDisposable
    {

        public static SHA1 Create()
        {
            return new SHA1();
        }

        public void Dispose() {}

#if NETFX_CORE        
        private HashAlgorithmProvider hap;

        private SHA1()
        {
            hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
        }

        public byte[] ComputeHash(byte[] buffer)
        {
            IBuffer buffInput = CryptographicBuffer.CreateFromByteArray(buffer);
            IBuffer buffHash = hap.HashData(buffInput);

            byte[] hash;
            CryptographicBuffer.CopyToByteArray(buffHash, out hash);
            return hash;
        }
#else
        public byte[] ComputeHash(byte[] buffer) { throw new System.NotImplementedException(); }
#endif
    }
}
