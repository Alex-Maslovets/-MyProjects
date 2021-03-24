using System;
using System.Collections.Generic;
using Sharp7;

namespace ReadWriteS7
{
    public class ReadS7
    {
        #region Считывание по одной переменной
        #region Считывание из DB-области (DataBlocks)
        public double DB_ReadReal(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.DBRead(DBNumber, Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Real from DB" + DBNumber + ".DBD" + Start + " Text: "+ client.ErrorText(result));
            }
            double value = S7.GetRealAt(DBBuffer, 0);
            return value;
        }
        public double DB_ReadDword(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.DBRead(DBNumber, Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
            }
            double value = S7.GetDWordAt(DBBuffer, 0);
            return value;
        }
        public string DB_ReadDwordHex(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.DBRead(DBNumber, Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord(Hex) from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
            }
            double value = S7.GetDWordAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public int DB_ReadInt(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.DBRead(DBNumber, Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Int from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            int value = S7.GetIntAt(DBBuffer, 0);
            return value;
        }
        public ushort DB_ReadWord(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.DBRead(DBNumber, Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            ushort value = S7.GetWordAt(DBBuffer, 0);
            return value;
        }
        public string DB_ReadWordHex(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.DBRead(DBNumber, Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word(Hex) from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            ushort value = S7.GetWordAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public byte DB_ReadByte(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.DBRead(DBNumber, Start, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte from DB" + DBNumber + ".DBB" + Start + " Text: " + client.ErrorText(result));
            }
            byte value = S7.GetByteAt(DBBuffer, 0);
            return value;
        }
        public string DB_ReadByteHex(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.DBRead(DBNumber, Start, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte(Hex) from DB" + DBNumber + ".DBB" + Start + " Text: " + client.ErrorText(result));
            }
            byte value = S7.GetByteAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public bool DB_ReadBit(S7Client client, int DBNumber, int StartByte, int Bit)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.DBRead(DBNumber, StartByte, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Bit from DB" + DBNumber + ".DBX" + StartByte + "." + Bit + " Text: " + client.ErrorText(result));
            }
            bool value = S7.GetBitAt(DBBuffer, 0, Bit);
            return value;
        }
        #endregion
        #region Считывание из М-области (Меркерная память)
        public double M_ReadReal(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.MBRead(Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Real from MD" + Start + " Text: " + client.ErrorText(result));
            }
            double value = S7.GetRealAt(DBBuffer, 0);
            return value;
        }
        public double M_ReadDword(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.MBRead(Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord from MD" + Start + " Text: " + client.ErrorText(result));
            }
            double value = S7.GetDWordAt(DBBuffer, 0);
            return value;
        }
        public string M_ReadDwordHex(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.MBRead( Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord(Hex) from MD" + Start + " Text: " + client.ErrorText(result));
            }
            double value = S7.GetDWordAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public int M_ReadInt(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.MBRead(Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Int from MW" + Start + " Text: " + client.ErrorText(result));
            }
            int value = S7.GetIntAt(DBBuffer, 0);
            return value;
        }
        public ushort M_ReadWord(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.MBRead(Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word from MW" + Start + " Text: " + client.ErrorText(result));
            }
            ushort value = S7.GetWordAt(DBBuffer, 0);
            return value;
        }
        public string M_ReadWordHex(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[2];
            int result = client.MBRead(Start, 2, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word(Hex) from MW" + Start + " Text: " + client.ErrorText(result));
            }
            ushort value = S7.GetWordAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public byte M_ReadByte(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.MBRead(Start, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte from MB" + Start + " Text: " + client.ErrorText(result));
            }
            byte value = S7.GetByteAt(DBBuffer, 0);
            return value;
        }
        public string M_ReadByteHex(S7Client client, int Start)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.MBRead(Start, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte(Hex) from MB" + Start + " Text: " + client.ErrorText(result));
            }
            byte value = S7.GetByteAt(DBBuffer, 0);
            return value.ToString("X");
        }
        public bool M_ReadBit(S7Client client, int StartByte, int Bit)
        {
            byte[] DBBuffer = new byte[1];
            int result = client.MBRead(StartByte, 1, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Bit from M" + StartByte + "." + Bit + " Text: " + client.ErrorText(result));
            }
            bool value = S7.GetBitAt(DBBuffer, 0, Bit);
            return value;
        }
        #endregion
        #endregion

        #region Считывание массивов
        #region Считывание из DB-области (DataBlocks)
        public List<double> DB_ReadRealArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List <double> tempList = new List<double>();
            byte[] DBBuffer = new byte[4*Size];
            int result = client.DBRead(DBNumber, Start, 4*Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Real Array from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetRealAt(DBBuffer, 4*i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<double> DB_ReadDWordArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<double> tempList = new List<double>();
            byte[] DBBuffer = new byte[4 * Size];
            int result = client.DBRead(DBNumber, Start, 4 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord Array from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetDWordAt(DBBuffer, 4 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> DB_ReadDWordHexArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[4 * Size];
            int result = client.DBRead(DBNumber, Start, 4 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord(Hex) Array from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetDWordAt(DBBuffer, 4 * i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<int> DB_ReadIntArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<int> tempList = new List<int>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.DBRead(DBNumber, Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Int Array from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                int value = S7.GetIntAt(DBBuffer, 2 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<ushort> DB_ReadWordArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<ushort> tempList = new List<ushort>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.DBRead(DBNumber, Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word Array from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                ushort value = S7.GetWordAt(DBBuffer, 2 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> DB_ReadWorHexdArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.DBRead(DBNumber, Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word(Hex) Array from DB" + DBNumber + ".DBW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                ushort value = S7.GetWordAt(DBBuffer, 2 * i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<byte> DB_ReadByteArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<byte> tempList = new List<byte>();
            byte[] DBBuffer = new byte[Size];
            int result = client.DBRead(DBNumber, Start, Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte Array from DB" + DBNumber + ".DBB" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                byte value = S7.GetByteAt(DBBuffer, i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> DB_ReadByteHexArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[Size];
            int result = client.DBRead(DBNumber, Start, Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte(Hex) Array from DB" + DBNumber + ".DBB" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                byte value = S7.GetByteAt(DBBuffer, i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<bool> DB_ReadBitArray(S7Client client, int DBNumber, int StartByte, int StartBit, int Size)
        {
            List<bool> tempList = new List<bool>();
            
            int quotient = Math.DivRem(StartBit + Size, 8, out int remainder);
            
            byte[] DBBuffer = new byte[remainder == 0 ? quotient : quotient + 1];
            int result = client.DBRead(DBNumber, StartByte, quotient, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Bit Array from DB" + DBNumber + ".DBX" + StartByte + "." + StartBit + " Text: " + client.ErrorText(result));
            }
            for (int i = StartBit; i < StartBit + Size; i++)
            {
                int quotientFor = Math.DivRem(i, 8, out int remainderFor);
                int temp_Pos = quotientFor;
                int temp_Bit = i - 8 * quotientFor;

                bool value = S7.GetBitAt(DBBuffer, temp_Pos, temp_Bit);

                tempList.Add(value);
            }
            return tempList;
        }
        #endregion
        #region Считывание из М-области (Меркерная память)
        public List<double> M_ReadRealArray(S7Client client, int Start, int Size)
        {
            List<double> tempList = new List<double>();
            byte[] DBBuffer = new byte[4 * Size];
            int result = client.MBRead(Start, 4 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Real Array from MD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetRealAt(DBBuffer, 4 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<double> M_ReadDWordArray(S7Client client, int Start, int Size)
        {
            List<double> tempList = new List<double>();
            byte[] DBBuffer = new byte[4 * Size];
            int result = client.MBRead(Start, 4 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord Array from MD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetDWordAt(DBBuffer, 4 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> M_ReadDWordHexArray(S7Client client, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[4 * Size];
            int result = client.MBRead(Start, 4 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read DWord(Hex) Array from MD" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                double value = S7.GetDWordAt(DBBuffer, 4 * i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<int> M_ReadIntArray(S7Client client, int Start, int Size)
        {
            List<int> tempList = new List<int>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.MBRead(Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Int Array from MW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                int value = S7.GetIntAt(DBBuffer, 2 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<ushort> M_ReadWordArray(S7Client client, int Start, int Size)
        {
            List<ushort> tempList = new List<ushort>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.MBRead( Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word Array from MW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                ushort value = S7.GetWordAt(DBBuffer, 2 * i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> M_ReadWorHexdArray(S7Client client, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[2 * Size];
            int result = client.MBRead(Start, 2 * Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Word(Hex) Array from MW" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                ushort value = S7.GetWordAt(DBBuffer, 2 * i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<byte> M_ReadByteArray(S7Client client, int Start, int Size)
        {
            List<byte> tempList = new List<byte>();
            byte[] DBBuffer = new byte[Size];
            int result = client.MBRead(Start, Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte Array from MB" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                byte value = S7.GetByteAt(DBBuffer, i);
                tempList.Add(value);
            }
            return tempList;
        }
        public List<string> M_ReadByteHexArray(S7Client client, int Start, int Size)
        {
            List<string> tempList = new List<string>();
            byte[] DBBuffer = new byte[Size];
            int result = client.MBRead(Start, Size, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Byte(Hex) Array from MB" + Start + " Text: " + client.ErrorText(result));
            }
            for (int i = 0; i < Size; i++)
            {
                byte value = S7.GetByteAt(DBBuffer, i);
                tempList.Add(value.ToString("X"));
            }
            return tempList;
        }
        public List<bool> M_ReadBitArray(S7Client client, int StartByte, int StartBit, int Size)
        {
            List<bool> tempList = new List<bool>();

            int quotient = Math.DivRem(StartBit + Size, 8, out int remainder);

            byte[] DBBuffer = new byte[remainder == 0 ? quotient : quotient + 1];
            int result = client.MBRead(StartByte, quotient, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Bit Array from M" + StartByte + "." + StartBit + " Text: " + client.ErrorText(result));
            }
            for (int i = StartBit; i < StartBit + Size; i++)
            {
                int quotientFor = Math.DivRem(i, 8, out int remainderFor);
                int temp_Pos = quotientFor;
                int temp_Bit = i - 8 * quotientFor;

                bool value = S7.GetBitAt(DBBuffer, temp_Pos, temp_Bit);

                tempList.Add(value);
            }
            return tempList;
        }
        #endregion
        #endregion
    }
}
