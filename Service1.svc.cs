using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
namespace WcfService1
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

        public bool DeleteApplication(int applicationId)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var order = tables.Orders.Single(c => c.Id == applicationId);
                tables.Orders.DeleteOnSubmit(order);
                tables.Orders.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return false;
        }
        public bool DeleteManager(int managerId)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerId == managerId);
                tables.Managers.DeleteOnSubmit(manager);
                tables.Managers.Context.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return false;
        }

        public bool UptadeApplication(ApplicationType composite)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var order = tables.Orders.Single(c => c.Id == composite.ApplicationId);

                order.ApplicationNumber = composite.ApplicationNumber;
                order.DateTime = composite.ApplicationDateTime;
                order.ManagerId = composite.ManagerId;
                order.Status = composite.OperationStatus;
                
                tables.Orders.Context.SubmitChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return false;
        }

        public bool UpdateManager(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerId == composite.ManagerId);

                manager.ManagerName = composite.ManagerName;
                manager.ManagerPassword = composite.ManagerPassword;

                tables.Managers.Context.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return false;
        }

        public bool AddApplication(ApplicationType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Orders.All(x => x.DateTime != composite.ApplicationDateTime))
            {
              
                var order = new Orders
                    {
                        DateTime = composite.ApplicationDateTime,
                        ApplicationNumber = composite.ApplicationNumber,
                        Status = composite.OperationStatus,
                        ManagerId = composite.ManagerId
                    };

                try
                {
                    tables.Orders.InsertOnSubmit(order);
                    tables.Orders.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public bool AddManager(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Managers.All(x => x.ManagerName != composite.ManagerName))
            {

                var manager = new Managers()
                    {
                        ManagerName = composite.ManagerName,
                        ManagerPassword = composite.ManagerPassword
                    };

                try
                {
                    tables.Managers.InsertOnSubmit(manager);
                    tables.Managers.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}