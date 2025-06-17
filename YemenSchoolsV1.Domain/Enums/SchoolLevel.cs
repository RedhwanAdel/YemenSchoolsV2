using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Domain.Enums
{
    [Flags]
    public enum SchoolLevel
    {
        None = 0,
        Kindergarten = 1,
        Elementary = 2,
        Middle = 4,
        High = 8
    }
}
