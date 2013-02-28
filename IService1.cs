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
    public interface IService1
    {

        [OperationContract]
        List<string> GetNewOperation();

        [OperationContract]
        bool AddOrder(OrderType composite);

        [OperationContract]
        bool AddManager(ManagerType composite);

        [OperationContract]
        bool DeleteOrder(OrderType composite);

        [OperationContract]
        bool DeleteManager(ManagerType composite);

        [OperationContract]
        bool UptadeOrder(OrderType composite);

        [OperationContract]
        bool UpdateManager(ManagerType composite);

        [OperationContract]
        int SignIn(ManagerType composite);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class OrderType
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string OrderArticle { get; set; }

        [DataMember]
        public DateTime OrderDateTime { get; set; }

        [DataMember]
        public int ManagerId { get; set; }
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class ManagerType
    {
        [DataMember]
        public int ManagerId { get; set; }

        [DataMember]
        public string ManagerName { get; set; }

        [DataMember]
        public string ManagerPassword { get; set; }
    }
}
