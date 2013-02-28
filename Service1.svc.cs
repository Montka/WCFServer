using System;
using System.Collections.Generic;
using System.Linq;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IService1
    {
        private const string ConnetionString =
            "Server=c40aff4d-5190-477c-812b-a12200a0119b.sqlserver.sequelizer.com;Database=dbc40aff4d5190477c812ba12200a0119b;User ID=doubddyerrnzaefz;Password=YHdzb6Ugt5ZVe35A6iAKQMLNZARqSpzbnHsmoSEkXPRmojeVSf72QJok5xGHoCve;";

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

        private void AddLog(string type, string message, DateTime datetime)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                String command =
                    String.Format(
                        "INSERT INTO dbo.Log(Type,Message,Date,Time) VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\')",
                        type, message, datetime.ToString("yyyy-MM-dd"),
                        datetime.ToString("hh:mm"));
                tables.ExecuteCommand(command);

                //tables.Orders.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                AddLog("system", "Ошибка при добавление записи в лог", DateTime.Now);
            }
        }

        public bool DeleteOrder(OrderType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var order = tables.Orders.Single(c => c.Id == composite.OrderId);
                tables.Orders.DeleteOnSubmit(order);
                tables.Orders.Context.SubmitChanges();

                message = "Заявка удалена : " + composite.OrderArticle;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при удалении заявки : " + composite.OrderArticle;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now);
            }

            /*
                        return false;
            */
        }

        public bool DeleteManager(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerId == composite.ManagerId);
                tables.Managers.DeleteOnSubmit(manager);
                tables.Managers.Context.SubmitChanges();

                message = "Пользователь удален : " + composite.ManagerName;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошиюка при удаление пользователя : " + composite.ManagerName;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now);
            }
            /*
                        return false;
            */
        }

        public bool UptadeOrder(OrderType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var order = tables.Orders.Single(c => c.Id == composite.OrderId);

                order.Article = composite.OrderArticle;
                order.Date = DateTime.Parse(composite.OrderDateTime.ToString("yy-MM-dd"));
                order.ManagerId = composite.ManagerId;
                order.Time = TimeSpan.Parse(composite.OrderDateTime.ToString("hh:mm"));

                tables.Orders.Context.SubmitChanges();

                message = "Обновление заявки : " + composite.OrderArticle;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при обновлении заявки : " + composite.OrderArticle;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now);
            }
            /*
                        return false;
            */
        }

        public bool UpdateManager(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerId == composite.ManagerId);

                manager.ManagerName = composite.ManagerName;
                manager.ManagerPassword = composite.ManagerPassword;

                tables.Managers.Context.SubmitChanges();

                message = "Обновление пользователя : " + composite.ManagerName;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при обновление пользователя : " + composite.ManagerName;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now);
            }
            /*
                        return false;
            */
        }

        public int SignIn(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Managers.Single(c => c.ManagerName == composite.ManagerName);
                int number;
                if (manager.ManagerPassword == composite.ManagerPassword)
                {
                    message = "Вход в систему : " + composite.ManagerName;
                    number = manager.ManagerId;
                }
                else
                {
                    message = "Ошибка входа в систему : " + composite.ManagerName;
                    number = -1;
                }

                return number;
            }
            catch (Exception ex)
            {
                message = "Ошибка при входе пользователя в систему : " + composite.ManagerName;
                type = "system";
                return -1;
            }
            finally
            {
                AddLog(type, message, DateTime.Now);
            }
        }

        public bool AddOrder(OrderType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Orders.All(x => x.Date != composite.OrderDateTime && x.Time != TimeSpan.Parse(composite.OrderDateTime.ToString("hh:mm"))))
            {
                string type = "user", message = string.Empty;

                var order = new Orders
                    {
                        ManagerId = composite.ManagerId,
                        Article = composite.OrderArticle,
                        Date = DateTime.Parse(composite.OrderDateTime.ToString("yy-MM-dd")),
                        Time = TimeSpan.Parse(composite.OrderDateTime.ToString("hh:mm"))
                    };

                try
                {
                    String command =
                        String.Format(
                            "INSERT INTO dbo.Orders(Article,ManagerId,Date,Time) VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\')",
                            order.Article,
                            order.ManagerId,
                            order.Date,
                            order.Time
                            );

                    tables.ExecuteCommand(command);

                    message = "Добавлена новая заявка : " + order.Article;
                    //tables.Orders.Context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    message = "Ошибка при добавление заявки : " + order.Article;
                    type = "system";
                    Console.WriteLine(ex.Message);
                    return false;
                }
                finally
                {
                    AddLog(type, message, DateTime.Now);
                }
            }
            return false;
        }

        public bool AddManager(ManagerType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Managers.All(x => x.ManagerName != composite.ManagerName))
            {
                string type="user", message=string.Empty;

                var manager = new Managers()
                    {
                        ManagerName = composite.ManagerName,
                        ManagerPassword = composite.ManagerPassword
                    };

                try
                {
                    tables.Managers.InsertOnSubmit(manager);
                    tables.Managers.Context.SubmitChanges();

                    message = "Добавлен новый пользователь : " + composite.ManagerName;
                    return true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    message = "Ошибка при добавление пользователя : " + composite.ManagerName;
                    type = "system";
                    return false;
                }
                finally
                {
                    AddLog(type, message, DateTime.Now);
                }
            }
            return false;
        }
    }
}