using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfServer
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        private const string ConnetionString = "Server=c40aff4d-5190-477c-812b-a12200a0119b.sqlserver.sequelizer.com;Database=dbc40aff4d5190477c812ba12200a0119b;User ID=doubddyerrnzaefz;Password=YHdzb6Ugt5ZVe35A6iAKQMLNZARqSpzbnHsmoSEkXPRmojeVSf72QJok5xGHoCve;";

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public ApplicationType GetDataUsingDataContract(ApplicationType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            return composite;
        }

        public int InsertData(ApplicationType composite)
        {
            var cnn = new SqlConnection(ConnetionString);

            const string sqlIns = "INSERT INTO dbo.ORDERS(Manager,ApplicationNumber,DateTime,Status) VALUES (@manager,@number,@datetime,@status)";
            try
            {
                cnn.Open();

                if (!CanInsertData(composite.ApplicationDateTime))
                    return -1;

                var cmdIns = new SqlCommand(sqlIns, cnn);

                cmdIns.Parameters.AddWithValue("@manager", composite.ManagerName);
                cmdIns.Parameters.AddWithValue("@number", composite.ApplicationNumber);
                cmdIns.Parameters.AddWithValue("@datetime", composite.ApplicationDateTime);
                cmdIns.Parameters.AddWithValue("@status", composite.OperationStatus);
                cmdIns.ExecuteNonQuery();
                cmdIns.Parameters.Clear();


                //// Get the last inserted id.
                cmdIns.CommandText = "SELECT @@IDENTITY";
                var insertId = Convert.ToInt32(cmdIns.ExecuteScalar());
                
                cmdIns.Dispose();
                cnn.Close();

                return insertId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sql Connection on Inset failed");
                return -1;
            }
            return -1;
        }

        public bool CanInsertData(DateTime insertDateTime)
        {
            var cnn = new SqlConnection(ConnetionString);
           
            const string sqlIns = "SELECT DateTime FROM dbo.ORDERS";
            try
            {
                bool ican = true;
                cnn.Open();
                var cmdIns = new SqlCommand(sqlIns, cnn);

                using (var reader = cmdIns.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dt = reader.GetDateTime(0);
                        if (insertDateTime == dt)
                        {
                            ican = false;
                            break;
                        }
                    }
                    reader.Close();
                }

                cmdIns.Dispose();
                cnn.Close();
                
                return ican;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Sql Connection on Inset failed");
                return false;
            }

            return false;
        }

    }
    
}
