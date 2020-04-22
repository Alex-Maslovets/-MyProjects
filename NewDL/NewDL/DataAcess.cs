using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Runtime.InteropServices;

namespace NewDL
{
    public struct OdbcSource
    {
        public string ServerName;
        public string DriverName;
    }

    class DataAcess
    {
        public static class OdbcWrapper
        {
            [DllImport("odbc32.dll")]
                public static extern int SQLDataSources(int EnvHandle, int Direction, StringBuilder ServerName, int ServerNameBufferLenIn, ref int ServerNameBufferLenOut, StringBuilder Driver, int DriverBufferLenIn, ref int DriverBufferLenOut);
            [DllImport("odbc32.dll")]
                public static extern int SQLAllocEnv(ref int EnvHandle);
        }

        public static List<OdbcSource> ListODBCsources()
        {
            int envHandle = 0;
            const int SQL_FETCH_NEXT = 1;
            const int SQL_FETCH_FIRST_SYSTEM = 32;
            List<OdbcSource> ListSources = new List<OdbcSource>();
            
            if (OdbcWrapper.SQLAllocEnv(ref envHandle) != -1)
            {
                int ret;
                StringBuilder serverName = new StringBuilder(1024);
                StringBuilder driverName = new StringBuilder(1024);
                int snLen = 0;
                int driverLen = 0;
                ret = OdbcWrapper.SQLDataSources(envHandle, SQL_FETCH_FIRST_SYSTEM, serverName, serverName.Capacity, ref snLen, driverName, driverName.Capacity, ref driverLen);
                while (ret == 0)
                {
                    OdbcSource Source = new OdbcSource();
                    Source.ServerName = serverName.ToString();
                    Source.DriverName = driverName.ToString();
                    ListSources.Add(Source);
                    ret = OdbcWrapper.SQLDataSources(envHandle, SQL_FETCH_NEXT, serverName, serverName.Capacity, ref snLen, driverName, driverName.Capacity, ref driverLen);
                }
            }
            return ListSources;
        }

        static public void InsertRow(string connectionString)
        {
            string queryString = "INSERT INTO Customers (CustomerID, CompanyName) Values('NWIND', 'Northwind Traders')";
            
            OdbcCommand command = new OdbcCommand(queryString);

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();

                // The connection is automatically closed at 
                // the end of the Using block.
            }
        }
    }
}
