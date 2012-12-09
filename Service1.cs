using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace Services
{
	[ServiceBehavior(IncludeExceptionDetailInFaults=true,
		InstanceContextMode=InstanceContextMode.Single)]
	public class Service1 : IService1
	{
        private string MainCommand;
        //**********************************************
        //          Передача Команды
        //**********************************************
        public string GetCommand()
        {
            return null;
        }
        //**********************************************
        //          Получение Текущей команды
        //**********************************************
        public void SetCommand(string value)
        {
            MainCommand = value;
        }
	}
}
