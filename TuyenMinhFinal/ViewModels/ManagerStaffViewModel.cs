using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuyenMinhFinal.ViewModels
{
    public class ManagerStaffViewModel
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        
        public int Phone { get; set; }
        public List<ManagerStaffViewModel> Trainee { get; set; }
        public List<ManagerStaffViewModel> Trainer { get; set; }
        public object[] Id { get; internal set; }
    }
}