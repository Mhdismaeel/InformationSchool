using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InformationSchool.Model.DTO
{
    public class DataWithError
    {
        public object Result { get; set; }

        public string ErrorMessage { get; set; }
    }
}
