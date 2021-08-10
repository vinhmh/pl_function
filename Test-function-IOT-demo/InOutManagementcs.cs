using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_function_IOT_demo
{
    public class InOutManagementcs
    {
        public string device_id;
        public string uid;
        public string user_info;
        public string status;
        public string date;

        public InOutManagementcs(string device_id, string uid, string user_info, string status, string date)
        {
            this.device_id = device_id;
            this.uid = uid;
            this.user_info = user_info;
            this.status = status;
            this.date = date;
        }

    }
}
