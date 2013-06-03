using System;
using System.Collections.Generic;
using System.Linq;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class MainService : IMainService
    {
        private const string ConnetionString =
            "Server=4343f352-8bf4-4470-ac22-a18100f83ceb.sqlserver.sequelizer.com;Database=db4343f3528bf44470ac22a18100f83ceb;User ID=unnyyulnscyafkss;Password=2StsmeparVFAphAnha7y2XKVNmyiR5DEfapxHfCSrEPjparKMT3mztYcgFL8Mgrv;";

        private void AddLog(string type, string message, string datetime)
        {
            var tables = new LinqWorkerDataContext();
            try
            {
                String command =
                    String.Format(
                        "INSERT INTO dbo.Log(Type,Message,Date) VALUES(\'{0}\',\'{1}\',\'{2}\')",
                        type, message, datetime);
                tables.ExecuteCommand(command);

                //tables.Orders.Context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool DeleteOrder(OrderType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var order = tables.Orders.Single(c => c.Id == composite.Id);
                tables.Orders.DeleteOnSubmit(order);
                tables.Orders.Context.SubmitChanges();

                message = "Заявка удалена : " + composite.Article;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при удалении заявки : " + composite.Article;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            }

            /*
                        return false;
            */
        }

        public bool DeleteManager(UserType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Users.Single(c => c.Id == composite.Id);
                tables.Users.DeleteOnSubmit(manager);
                tables.Users.Context.SubmitChanges();

                message = "Пользователь удален : " + composite.Fio;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при удаление пользователя : " + composite.Fio;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            }
            /*
                        return false;
            */
        }
        public bool DeleteLog(int id)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var log = tables.Log.Single(c => c.Id == id);
                tables.Log.DeleteOnSubmit(log);
                tables.Log.Context.SubmitChanges();

                message = "Запись лога удалена : " + id;
                return true;
            }
            catch (Exception ex)
            {
                message = "Ошибка при удаление записи лога : " + id;
                type = "system";
                return false;
            }
            finally
            {
               
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
                var order = tables.Orders.Single(c => c.Id == composite.Id);

                order.Article = composite.Article;
                order.Date = composite.Date;
                order.UserId = composite.UserId;
                order.Comment = composite.Comment;
                order.ItemId = composite.ItemId;
                order.ItemCount = composite.ItemCount;

                tables.Orders.Context.SubmitChanges();

                message = "Обновление заявки : " + composite.Article;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                message = "Ошибка при обновлении заявки : " + composite.Article;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            }
            /*
                        return false;
            */
        }

        public bool UpdateManager(UserType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Users.Single(c => c.Id == composite.Id);
           
                manager.Fio = composite.Fio;
                manager.Login = composite.Login;
                manager.Password = composite.Password;
            

                tables.Users.Context.SubmitChanges();

                message = "Обновление пользователя : " + composite.Fio;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                message = "Ошибка при обновление пользователя : " + composite.Fio;
                type = "system";
                return false;
            }
            finally
            {
                AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            }
            /*
                        return false;
            */
        }

        public int SignIn(UserType composite)
        {
            var tables = new LinqWorkerDataContext();
            string type = "user", message = string.Empty;
            try
            {
                var manager = tables.Users.Single(c => c.Login == composite.Login);
                int number;
                if (manager.Password == composite.Password)
                {
                    message = "Вход в систему : " + composite.Fio;
                    number = manager.Id;
                }
                else
                {
                    message = "Ошибка входа в систему : " + composite.Fio;
                    number = -1;
                }

                return number;
            }
            catch (Exception ex)
            {
                message = "Ошибка при входе пользователя в систему : " + composite.Fio;
                type = "system";
                return -1;
            }
            finally
            {
                AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
            }
        }
        public void Test()
        {
            
        }
        public bool AddOrder(OrderType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Orders.All(x => x.Date != composite.Date ))
            {
                string type = "user", message = string.Empty;

                var order = new Orders
                    {
                        UserId = composite.UserId,
                        Article = composite.Article,
                        Date =  composite.Date,
                        Comment = composite.Comment,
                        ItemCount =  composite.ItemCount,
                        ItemId = composite.ItemId
                    };

                try
                {
                    String command =
                        String.Format(
                            "INSERT INTO dbo.Orders(Article,UserId,Date,Comment,Itemid,ItemCount) VALUES(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\')",
                            order.Article,
                            order.UserId,
                            order.Date,
                            order.Comment,
                            order.ItemId,
                             order.ItemCount
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
                    AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                }
            }
            return false;
        }

        public bool AddManager(UserType composite)
        {
            var tables = new LinqWorkerDataContext();

            if (tables.Users.All(x => x.Login != composite.Login))
            {
                string type="user", message=string.Empty;

                var manager = new Users()
                    {
                        Fio = composite.Fio,
                        Login = composite.Login,
                        Password = composite.Password
                    };

                try
                {
                    String command =
                        String.Format(
                            "INSERT INTO dbo.Users(Fio,Login,Password) VALUES(\'{0}\',\'{1}\',\'{2}\')",
                            manager.Fio,
                            manager.Login,
                            manager.Password
                            );

                    tables.ExecuteCommand(command);

                    message = "Добавлен новый пользователь : " + composite.Fio;
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    message = "Ошибка при добавление пользователя : " + composite.Fio;
                    type = "system";
                    return false;
                }
                finally
                {
                    AddLog(type, message, DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                }
            }
            return false;
        }
    }
}