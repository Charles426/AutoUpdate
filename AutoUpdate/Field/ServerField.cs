using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate.Field
{
    public class ServerField
    {
        /// <summary>
        /// 发布日期
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// 发布链接
        /// </summary>
        public string ReleaseUrl { get; set; }

        /// <summary>
        /// 发布版本
        /// </summary>
        public string ReleaseVersion { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionDesc { get; set; }

    }
}
