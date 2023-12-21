using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24AdvertFormat
{
    internal class B24AdvertismentParser
    {
        public enum DataType
        {
            Unencoded,
            Encoded
        }

        #region properties

        public byte[] Seed { get; } = new byte[] { 0x5C, 0x6F, 0x2F, 0x41, 0x21, 0x7A, 0x26, 0x45, 0x5C, 0x6F};
        public byte[] ViewPIN { get; private set; } = new byte[] { 0x30, 0x30, 0x30, 0x30 };
    
        private byte[] _key = null;

        public B24Advertisment Advertisment { get; set; } = null;

        #endregion

        public bool SetPayload(DataType dt, byte[] data)
        {
            bool result = false;
            byte[] unencoded = null;
            if (13 == data.Length)
            {
                if (DataType.Encoded == dt)
                {
                    unencoded = new byte[data.Length];
                    unencoded[0] = data[0];
                    unencoded[1] = data[1];
                    unencoded[2] = data[2];
                    decodeData(ref unencoded, data, 3);
                }
                else
                {
                    unencoded = data;
                }
                if (1 == unencoded[0])
                {
                    Advertisment = new B24Advertisment();
                    Advertisment.ModuleID = new byte[] { unencoded[1], unencoded[2] };
                    Advertisment.Status = unencoded[3];
                    Advertisment.Units = unencoded[4];
                    Advertisment.Data = new byte[] { unencoded[5], unencoded[6], unencoded[7], unencoded[8] };
                    Advertisment.DataTag1 = new byte[] { unencoded[9], unencoded[10] };
                    Advertisment.DataTag2 = new byte[] { unencoded[11], unencoded[12] };
                    result = true;
                }
                else
                {
                    Advertisment = null;
                }

            }
            return result;
        }

        private void decodeData(ref byte[] unencoded, byte[] data, int ofs)
        {
            for (int i = 0; i < _key.Length; i++)
            {
                unencoded[ofs + i] = (byte)(_key[i] ^ data[ofs + i]);
            }
        }

        public B24AdvertismentParser() : this("0000")
        {
        }

        public B24AdvertismentParser(string viewPin)
        {
            ViewPIN = Encoding.ASCII.GetBytes(viewPin);
            GenerateKey();
        }

        public B24Advertisment Parse(byte[] data)
        {
            B24Advertisment advert = new B24Advertisment();
            if (advert.Length != data[0])
            {
                throw new FormatException("Invalid length");
            }
            if (advert.AdvertType != data[1])
            {
                throw new FormatException("Invalid type");
            }
            if (advert.CompanyID != BitConverter.ToUInt16(data, 2))
            {
                throw new FormatException("Invalid company id");
            }
            if (advert.FormatID != data[4])
            {
                throw new FormatException("Unsupported format");
            }
            advert.ModuleID = new byte[] { data[5], data[6] };
            advert.Status = data[7];
            advert.Units = data[8];
            advert.Data = new byte[] { data[9], data[10], data[11], data[12] };
            advert.DataTag1 = new byte[] { data[13], data[14] };
            advert.DataTag2 = new byte[] { data[15], data[16] };
            return advert;
        }

        #region private methods
        private void GenerateKey()
        {
            byte[] key = new byte[Seed.Length];
            for (int i = 0; i < Seed.Length; i++)
            {
                key[i] = (byte)(Seed[i] ^ ViewPIN[i % ViewPIN.Length]);
            }
            _key = key;
        }

        #endregion
    }
}
