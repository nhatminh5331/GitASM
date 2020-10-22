using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuyenMinhFinal.Models;

namespace TuyenMinhFinal.ViewModels
{
    public class TrainerTopicViewModel
    {
        public TrainerTopic TrainerTopic { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
        public IEnumerable<Topic> Topics { get; set; }

    }
}