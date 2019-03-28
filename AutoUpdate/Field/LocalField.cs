using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Field
{
    public class LocalField
    {
        /// <summary>
        /// 本地版本
        /// </summary>
        public string LocalVersion { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public string LastUpdate { get; set; }

        /// <summary>
        /// 服务器server.xml文件地址
        /// </summary>
        public string ServerUpdateUrl { get; set; }


    }
}
