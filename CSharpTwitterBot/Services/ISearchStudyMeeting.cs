using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTwitterBot.Models;

namespace CSharpTwitterBot.Services
{
    public interface ISearchStudyMeeting
    {
        Uri ApiBaseUri { get; }
        Task<IEnumerable<StudyMeeting>> GetStudyMeetingList(IEnumerable<string> keywords);
    }
}
