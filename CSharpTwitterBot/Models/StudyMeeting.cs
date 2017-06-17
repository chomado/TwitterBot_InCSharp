using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTwitterBot.Models
{
    public class StudyMeeting
    {
        public string Title { get; set; }
        public Uri EventPageUri { get; set; }
        public DateTime StartingAt { get; set; }
        public List<string> TwitterHashTags { get; set; }

    }
}
