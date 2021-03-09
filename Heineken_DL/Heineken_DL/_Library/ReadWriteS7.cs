using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;

namespace Heineken_DL._Library
{
    class ReadWriteS7
    {
        public double ReadReal(S7Client client, int DBNumber, int Start)
        {
            byte[] DBBuffer = new byte[4];
            int result = client.DBRead(DBNumber, Start, 4, DBBuffer);
            if (result != 0)
            {
                Console.WriteLine("Error: (Read Real from DB" + DBNumber + ".DBD" + Start + " Text: "+ client.ErrorText(result));
            }
            double Real = S7.GetRealAt(DBBuffer, 0);
            return Real;
        }


    }
}
