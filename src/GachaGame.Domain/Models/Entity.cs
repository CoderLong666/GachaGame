using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using NodaTime;

namespace GachaGame.Domain.Models
{
    public class SuperFastHashUInt16Hack
    {
        public static uint Hash(byte[] dataToHash)
        {
            var dataLength = dataToHash.Length;
            if (dataLength == 0)
                return 0;
            var hash = (uint)dataLength;
            var remainingBytes = dataLength & 3;
            var numberOfLoops = dataLength >> 2;
            var currentIndex = 0;
            var arrayHack = new BytetoUInt16Converter { Bytes = dataToHash }.UInts;
            while (numberOfLoops > 0)
            {
                hash += arrayHack![currentIndex++];
                var tmp = (uint)(arrayHack[currentIndex++] << 11) ^ hash;
                hash = (hash << 16) ^ tmp;
                hash += hash >> 11;
                numberOfLoops--;
            }

            currentIndex *= 2;
            switch (remainingBytes)
            {
                case 3:
                    hash += (ushort)(dataToHash[currentIndex++] | (dataToHash[currentIndex++] << 8));
                    hash ^= hash << 16;
                    hash ^= (uint)dataToHash[currentIndex] << 18;
                    hash += hash >> 11;
                    break;
                case 2:
                    hash += (ushort)(dataToHash[currentIndex++] | (dataToHash[currentIndex] << 8));
                    hash ^= hash << 11;
                    hash += hash >> 17;
                    break;
                case 1:
                    hash += dataToHash[currentIndex];
                    hash ^= hash << 10;
                    hash += hash >> 1;
                    break;
            }

            hash ^= hash << 3;
            hash += hash >> 5;
            hash ^= hash << 4;
            hash += hash >> 17;
            hash ^= hash << 25;
            hash += hash >> 6;

            return hash;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct BytetoUInt16Converter
        {
            [FieldOffset(0)] public byte[] Bytes;

            [FieldOffset(0)] public readonly ushort[] UInts;
        }
    }

    public abstract class Entity
    {
        private static readonly IdGen.IdGenerator IdGenerator = new((int)((SuperFastHashUInt16Hack.Hash(Encoding.ASCII.GetBytes(Dns.GetHostName())) % 32) << 5) + (Environment.ProcessId % 32));

        protected Entity()
        {
            Id = IdGenerator.CreateId();
        }
        [Key] public long Id { get; set; }

        public Instant CreatedAt { get; set; }

        [Required] public string CreatedBy { get; set; } = string.Empty;

        public Instant? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
