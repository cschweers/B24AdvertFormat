using System;

namespace B24AdvertFormat
{
    internal class B24Advertisment
    {

        #region Properties
        public byte Length { get; } = 16;                               // field 1 byte Part of the Spec
        public byte AdvertType { get; } = 0xFF;                         // 1 byte 0xFF (Manufacturer Specific Data)
        public UInt16 CompanyID { get; } = 0x04C3;                      // 2 bytes 0x04C3
        public byte FormatID { get; } = 1;                              // byte
        public byte[] ModuleID { get; set; } = new byte[] { 0, 0 };     // 2 bytes Data Tag
        public byte Status { get; set; } = 0;                           // 1 byte
        public byte Units { get; set; } = 0;                            // 1 byte
        public byte[] Data { get; set; } = new byte[] { 0, 0, 0, 0 };   // 4 bytes Floating point data(IEEE 754)
        public byte[] DataTag1 { get; set; } = new byte[] { 0, 0 };     // 2 bytes Used to verify decoding
        public byte[] DataTag2 { get; set; } = new byte[] { 0, 0 };     // 2 bytes Used to verify decoding
        #endregion
    }
}
