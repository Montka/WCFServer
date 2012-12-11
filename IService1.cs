using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NewWcfServer
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        ApplicationType GetDataUsingDataContract(ApplicationType composite);

        [OperationContract]
        int InsertData(ApplicationType composite);

        // TODO: Добавьте здесь операции служб
    }


    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class ApplicationType
    {
        [DataMember]
        public int ApplicationId { get; set; }

        [DataMember]
        public string ManagerName { get; set; }

        [DataMember]
        public int ApplicationNumber { get; set; }

        [DataMember]
        public DateTime ApplicationDateTime { get; set; }

        [DataMember]
        public int OperationStatus { get; set; }

    }
}
