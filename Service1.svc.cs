using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        private const string ConnetionString = "Server=c40aff4d-5190-477c-812b-a12200a0119b.sqlserver.sequelizer.com;Database=dbc40aff4d5190477c812ba12200a0119b;User ID=doubddyerrnzaefz;Password=YHdzb6Ugt5ZVe35A6iAKQMLNZARqSpzbnHsmoSEkXPRmojeVSf72QJok5xGHoCve;";

        public List<String> GetNewOperation()
        {
            var tables = new LinqWorkerDataContext();
            var list = new List<String>();
            try
            {
                var ch = tables.Changes;
                list.AddRange(ch.Select(changese => changese.OrderId.ToString()));

                tables.ExecuteCommand("DELETE FROM dbo.Changes");

                return list;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return list;
            }
   
        }
        public bool DeleteApplication(int applicationId)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var order = tables.Orders.Single(c => c.Id == applicationId);
                tables.Orders.DeleteOnSubmit(order);
                tables.Orders.Context.SubmitChanges();

                var text = " Заявка № " + applicationId + " удалена ";
                var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                var resp = reqGet.GetResponse();


                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
   
/*
            return false;
*/
        }
        public bool DeleteManager(int managerId)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerId == managerId);
                tables.Managers.DeleteOnSubmit(manager);
                tables.Managers.Context.SubmitChanges();

                var text = "Мэнэджер № " + managerId + "удален";
                var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                var resp = reqGet.GetResponse();

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
/*
            return false;
*/
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

                var text = "Заявка изменена №" + order.Id + "остальное потом :)";
                var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                var resp = reqGet.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
/*
            return false;
*/
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

                var text = "Менеджер изменен" + composite.ManagerName +"остальное потом :)";
                var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                var resp = reqGet.GetResponse();

                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
/*
            return false;
*/
        }

        public int SignIn(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerName == composite.ManagerName);

                int number;
                string text;
                if (manager.ManagerPassword == composite.ManagerPassword)
                {
                   text = composite.ManagerName + " вошел в систему";
                    number = manager.ManagerId;
                }
                else 
                { 
                    text = composite.ManagerName + " ошибка входа в систему";
                    number = -1;
                }
                var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{"+text+"}");
                var resp = reqGet.GetResponse();
                return number;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return -1;
            }
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
                    var text = "заявка добавлена";
                    var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                    var resp = reqGet.GetResponse();
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
                    var text = "Менеджер добавлен";
                    var reqGet = System.Net.WebRequest.Create(@"http://montka.herokuapp.com/logging{" + text + "}");
                    var resp = reqGet.GetResponse();
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