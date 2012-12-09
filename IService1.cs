using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace Services
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetCommand();
        [OperationContract]
        void SetCommand(string value);
    }
}
