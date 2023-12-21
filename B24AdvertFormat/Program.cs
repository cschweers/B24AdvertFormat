using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B24AdvertFormat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            B24AdvertismentParser parser8742 = new B24AdvertismentParser("8742");
            B24AdvertismentParser parser0000 = new B24AdvertismentParser("0000");
            byte[] data1 = new byte[] { 0x01, 0x12, 0x34, 0x64, 0x75, 0x5B, 0x51, 0x96, 0x11, 0x00, 0x43, 0x76, 0x6C };
            byte[] data2 = new byte[] { 0x01, 0x66, 0x78, 0x6C, 0x5F, 0xDD, 0xF1, 0xD2, 0x60, 0x70, 0x0D, 0x0A, 0x27 };
            byte[] data3 = new byte[] { 0x01, 0x66, 0x78, 0x6C, 0x5F, 0xDD, 0x0E, 0xE7, 0x3C, 0x70, 0x0D, 0x0A, 0x27 };

            if (parser8742.SetPayload(B24AdvertismentParser.DataType.Encoded, data1))
            {
                B24Advertisment advert1 = parser8742.Advertisment;
                printRawData(data1);
                printAdvertisment(advert1);
            }
            else
            {
                Console.WriteLine("Payload not set");
            }
            if (parser0000.SetPayload(B24AdvertismentParser.DataType.Encoded, data2))
            {
                B24Advertisment advert2 = parser0000.Advertisment;
                printRawData(data2);
                printAdvertisment(advert2);
            }
            else
            {
                Console.WriteLine("Payload not set");
            }
            if (parser0000.SetPayload(B24AdvertismentParser.DataType.Encoded, data3))
            {
                B24Advertisment advert3 = parser0000.Advertisment;
                printRawData(data3);
                printAdvertisment(advert3);
            }
            else
            {
                Console.WriteLine("Payload not set");
            }
            if(Debugger.IsAttached)
            {
                Console.WriteLine("\npress any key to continue ...");
                Console.ReadKey();
            }
        }

        private static void printRawData(byte[] data)
        {
            Console.WriteLine("\nPayload raw data: {0}", BitConverter.ToString(data));
        }

        private static void printAdvertisment(B24Advertisment advert)
        {
            Console.WriteLine("Length: {0}", advert.Length);
            Console.WriteLine("AdvertType: 0x{0:X2}", advert.AdvertType);
            Console.WriteLine("CompanyID: 0x{0:X4}", advert.CompanyID);
            Console.WriteLine("FormatID: {0}", advert.FormatID);
            Console.WriteLine("ModuleID: {0}", BitConverter.ToString(advert.ModuleID));
            Console.WriteLine("Status: {0}", advert.Status);
            Console.WriteLine("Units: {0}", advert.Units);
            Console.WriteLine("Data: {0} ({1})", BitConverter.ToString(advert.Data), getFloatIeee754(advert.Data));
            Console.WriteLine("DataTag1: {0}", BitConverter.ToString(advert.DataTag1));
            Console.WriteLine("DataTag2: {0}", BitConverter.ToString(advert.DataTag2));
        }

        private static float getFloatIeee754(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data); // We have to reverse
            }
            return BitConverter.ToSingle(data, 0);
        }
    }
}
