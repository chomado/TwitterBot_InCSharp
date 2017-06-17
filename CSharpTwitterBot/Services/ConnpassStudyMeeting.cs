using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTwitterBot.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CSharpTwitterBot.Services
{
    public class ConnpassStudyMeeting : ISearchStudyMeeting
    {
        public Uri ApiBaseUri { get => new Uri("https://connpass.com/api/v1/event/"); }

        public async Task<List<StudyMeeting>> GetStudyMeetingList(List<string> keywords)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this.ApiBaseUri;

                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "Madoka");

                var parameter = keywords.Count > 1 
                    ? string.Join(separator: ",", values: keywords) 
                    : keywords.FirstOrDefault();

                Debug.WriteLine(parameter);

                var response = await client.GetAsync($"?keyword={parameter}");
                response.EnsureSuccessStatusCode();

                var connpassResponce = JsonConvert.DeserializeObject<Models.Connpass.RootObject>(await response.Content.ReadAsStringAsync());

                Console.WriteLine($"aaaaaaaaaaaaaaaaa {connpassResponce.Events.FirstOrDefault()?.Title}");
                
                return ConvertConnpassResponce2StudyMeetings(connpassResponce);
            }
        }

        private List<StudyMeeting> ConvertConnpassResponce2StudyMeetings(Models.Connpass.RootObject connpasses)
        {
            // note: connpass には複数の勉強会情報が入っている

            StudyMeeting ConvertConnpassEventToStudyMeeting(Models.Connpass.Event connpassEvent) => new StudyMeeting
            {
                Title = connpassEvent.Title,
                StartingAt = connpassEvent.StartedAt,
                EventPageUri = new Uri(connpassEvent.EventUrl),
                TwitterHashTags = new List<string>() { connpassEvent.HashTag }, // "JXUG #mspjp #gakusei_wakaru"
            };

            var studyMeetingList = new List<StudyMeeting>();

            foreach (var connpassEvent in connpasses.Events)
            {
                studyMeetingList.Add(ConvertConnpassEventToStudyMeeting(connpassEvent));
            }

            return studyMeetingList;
        }
    }
}
