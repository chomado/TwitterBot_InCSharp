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
        Task<List<StudyMeeting>> GetStudyMeetingList(List<string> keywords);
    }
}
