using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Models
{
    public interface ISqlRepository
    {
        bool AddUser(User user);
        bool UserExsist(User user);
    }
}
