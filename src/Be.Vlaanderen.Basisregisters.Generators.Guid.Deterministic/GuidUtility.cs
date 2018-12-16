// ReSharper disable once CheckNamespace
namespace Be.Vlaanderen.Basisregisters.Generators.Guid
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    // https://www.ietf.org/rfc/rfc4122.txt
    // https://en.wikipedia.org/wiki/Universally_unique_identifier#Versions_3_and_5_(namespace_name-based)
    // https://www.famkruithof.net/guid-uuid-namebased.html
    /// <summary>
    /// Helper methods for working with <see cref="Guid"/>.
    /// </summary>
    public static class Deterministic
    {
        /// <summary>
        /// Predefined namespace to generate deterministic <see cref="Guid"/>.
        /// </summary>
        public static class Namespaces
        {
            /// <summary>
            /// The namespace for Commands.
            /// </summary>
            public static readonly Guid Commands = Guid.Parse("b8bfc711-ed0b-4151-a4fd-26a749825f7b");

            /// <summary>
            /// The namespace for Events.
            /// </summary>
            public static readonly Guid Events = Guid.Parse("115a74c3-19dd-4753-b31e-f366eb3e2005");

            /// <summary>
            /// The namespace for fully-qualified domain names (from RFC 4122, Appendix C).
            /// </summary>
            public static readonly Guid Dns = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");

            /// <summary>
            /// The namespace for URLs (from RFC 4122, Appendix C).
            /// </summary>
            public static readonly Guid Url = new Guid("6ba7b811-9dad-11d1-80b4-00c04fd430c8");

            /// <summary>
            /// The namespace for ISO OIDs (from RFC 4122, Appendix C).
            /// </summary>
            public static readonly Guid IsoOid = new Guid("6ba7b812-9dad-11d1-80b4-00c04fd430c8");

            /// <summary>
            /// The namespace for X.500 DN (from RFC 4122, Appendix C).
            /// </summary>
            public static readonly Guid X500Dn = new Guid("6ba7b814-9dad-11d1-80b4-00c04fd430c8");
        }

        /// <summary>
        /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
        /// </summary>
        /// <param name="namespaceId">The ID of the namespace.</param>
        /// <param name="name">The name (within that namespace).</param>
        /// <returns>A UUID derived from the namespace and name.</returns>
        public static Guid Create(Guid namespaceId, string name) => Create(namespaceId, name, 5);

        /// <summary>
        /// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
        /// </summary>
        /// <param name="namespaceId">The ID of the namespace.</param>
        /// <param name="name">The name (within that namespace).</param>
        /// <param name="version">The version number of the UUID to create; this value must be either
        /// 3 (for MD5 hashing) or 5 (for SHA-1 hashing).</param>
        /// <returns>A UUID derived from the namespace and name.</returns>
        public static Guid Create(Guid namespaceId, string name, int version)
        {
            if (namespaceId == Guid.Empty)
                throw new ArgumentException("Namespace cannot be an empty GUID.", nameof(namespaceId));

            if (namespaceId == default(Guid))
                throw new ArgumentNullException(nameof(namespaceId), "Namespace cannot be null or empty.");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty.");

            if (version != 3 && version != 5)
                throw new ArgumentOutOfRangeException(nameof(version), "version must be either 3 or 5.");

            // convert the name to a sequence of octets (as defined by the standard or conventions of its namespace) (step 3)
            // ASSUME: UTF-8 encoding is always appropriate
            var nameBytes = Encoding.UTF8.GetBytes(name);

            if (nameBytes.Length == 0)
                throw new ArgumentNullException(nameof(nameBytes));

            // convert the namespace UUID to network order (step 3)
            var namespaceBytes = namespaceId.ToByteArray();
            SwapByteOrder(namespaceBytes);

            // compute the hash of the name space ID concatenated with the name (step 4)
            byte[] hash;
            using (var algorithm = version == 3 ? (HashAlgorithm) MD5.Create() : SHA1.Create())
            {
                var combinedBytes = new byte[namespaceBytes.Length + nameBytes.Length];
                Buffer.BlockCopy(namespaceBytes, 0, combinedBytes, 0, namespaceBytes.Length);
                Buffer.BlockCopy(nameBytes, 0, combinedBytes, namespaceBytes.Length, nameBytes.Length);

                hash = algorithm.ComputeHash(combinedBytes);
            }

            // most bytes from the hash are copied straight to the bytes of the new GUID (steps 5-7, 9, 11-12)
            var newGuid = new byte[16];
            Array.Copy(hash, 0, newGuid, 0, 16);

            // set the four most significant bits (bits 12 through 15) of the time_hi_and_version
            // field to the appropriate 4-bit version number from Section 4.1.3 (step 8)
            newGuid[6] = (byte) ((newGuid[6] & 0x0F) | (version << 4));

            // set the two most significant bits (bits 6 and 7) of the clock_seq_hi_and_reserved
            // to zero and one, respectively (step 10)
            newGuid[8] = (byte) ((newGuid[8] & 0x3F) | 0x80);

            // convert the resulting UUID to local byte order (step 13)
            SwapByteOrder(newGuid);
            return new Guid(newGuid);
        }

        // Converts a GUID (expressed as a byte array) to/from network order (MSB-first).
        private static void SwapByteOrder(byte[] guid)
        {
            SwapBytes(guid, 0, 3);
            SwapBytes(guid, 1, 2);
            SwapBytes(guid, 4, 5);
            SwapBytes(guid, 6, 7);
        }

        private static void SwapBytes(byte[] guid, int left, int right)
        {
            var temp = guid[left];
            guid[left] = guid[right];
            guid[right] = temp;
        }
    }
}
