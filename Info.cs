using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maimaiAddress
{
    public class Info
    {
        public string placeId { get; set; }
        public string id { get; set; }
        public string province { get; set; }
        public string arcadeName { get; set; }
        public string mall { get; set; }
        public string address { get; set; }
        public int machineCount { get; set; }
    }
    public class InfoDisplay
    {
        //public string 省份 { get; set; }
        public string 店铺名称 { get; set; }
        public string 地址 { get; set; }
        public int 机台数量 { get; set; }
    }
}
