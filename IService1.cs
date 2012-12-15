using System;
using System.Collections.Generic;
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
        bool AddApplication(ApplicationType composite);

        [OperationContract]
        bool AddManager(ManagerType composite);

        [OperationContract]
        bool DeleteApplication(int applicationId);

        [OperationContract]
        bool DeleteManager(int managerId);

        [OperationContract]
        bool UptadeApplication(ApplicationType composit);

        [OperationContract]
        bool UpdateManager(ManagerType composite);

        [OperationContract]
        bool SignIn(ManagerType composite);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class ApplicationType
    {
        [DataMember]
        public int ApplicationId { get; set; }

        [DataMember]
        public int ApplicationNumber { get; set; }

        [DataMember]
        public DateTime ApplicationDateTime { get; set; }

        [DataMember]
        public int ManagerId { get; set; }

        [DataMember]
        public int OperationStatus { get; set; }
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
