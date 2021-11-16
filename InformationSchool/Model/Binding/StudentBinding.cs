using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.Binding
{
    public class StudentBinding
    {
       public string StudentId { get; set; }
       public string FName { get; set; }
        public string LName { get; set; }

        public string UserName { get; set; }

        public string StudentEmail { get; set; }
       public string StudentPhone { get; set; }

        public string Address { get; set; }

        public string ImageProfile { get; set; }


        public DateTime UpdatedDate { get; set; }

    }
}
