using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCashCDN.Models
{
    public class ClassStudentVM
    {
        public Student Student { get; set; }

        public IList<Class> Classes { get; set; }
    }
}
