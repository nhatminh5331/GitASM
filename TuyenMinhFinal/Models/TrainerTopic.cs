using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuyenMinhFinal.Models
{
    public class TrainerTopic
    {
        public int Id { get; set; }
        public string TrainerId { get; set; }
        public int TopicId { get; set; }

        public ApplicationUser Trainer { get; set; }
        public Topic Topic { get; set; }

    }
}