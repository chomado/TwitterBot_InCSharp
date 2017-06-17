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

        public async Task<IEnumerable<StudyMeeting>> GetStudyMeetingList(IEnumerable<string> keywords)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = this.ApiBaseUri;

                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "Madoka");

                var parameter = string.Join(separator: ",", values: keywords);

                Debug.WriteLine(parameter);

                var response = await client.GetAsync($"?keyword={parameter}");
                response.EnsureSuccessStatusCode();

                var connpassResponce = JsonConvert.DeserializeObject<Models.Connpass.RootObject>(await response.Content.ReadAsStringAsync());

                Console.WriteLine($"{connpassResponce.Events.FirstOrDefault()?.Title}");
                
                return ConvertConnpassResponce2StudyMeetings(connpassResponce);
            }
        }

        private IEnumerable<StudyMeeting> ConvertConnpassResponce2StudyMeetings(Models.Connpass.RootObject connpasses)
        {
            // note: connpass には複数の勉強会情報が入っている

            StudyMeeting convertConnpassEventToStudyMeeting(Models.Connpass.Event connpassEvent) => new StudyMeeting
            {
                Title = connpassEvent.Title,
                StartingAt = connpassEvent.StartedAt,
                EventPageUri = new Uri(connpassEvent.EventUrl),
                TwitterHashTags = new [] { connpassEvent.HashTag }, // "JXUG #mspjp #gakusei_wakaru"
            };

            return connpasses.Events.Select(i => convertConnpassEventToStudyMeeting(i));
        }
    }
}
