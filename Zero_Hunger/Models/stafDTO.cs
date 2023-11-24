using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zero_Hunger.Models
{
    public class stafDTO
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
    }

}