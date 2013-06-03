using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IMainService
    {
        // TODO: Добавьте здесь операции служб

        [OperationContract]
        void Test();

        [OperationContract]
        bool AddOrder(OrderType composite);

        [OperationContract]
        bool AddManager(UserType composite);

        [OperationContract]
        bool DeleteOrder(OrderType composite);

        [OperationContract]
        bool DeleteManager(UserType composite);

        [OperationContract]
        bool DeleteLog(int id);

        [OperationContract]
        bool UptadeOrder(OrderType composite);

        [OperationContract]
        bool UpdateManager(UserType composite);

        [OperationContract]
        int SignIn(UserType composite);
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class OrderType
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Article { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public int ItemCount { get; set; }
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class UserType
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Fio { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
