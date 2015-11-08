using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using WebApplication1.Models;
namespace WebApplication1
{
    
    public class Elastic:IDisposable
    {
        ElasticClient client;
        Uri uri;
        public Elastic()
        {
            uri = new Uri("https://CoiDEDYkj0KsFMyFOQmrwYhyYOmyddEC:@georgemois123.east-us.azr.facetflow.io");
            var settings = new ConnectionSettings(uri).SetDefaultIndex("2_index");
            client = new ElasticClient(settings);
        }
        public void Dispose()
        {
        }
        public void Add(Object obj)
        {
            if(obj is Demotivator) client.Index(obj as Demotivator);
            if (obj is ApplicationUser) client.Index(obj as ApplicationUser);
            client.Refresh();
        }
        public void Delete(Object obj)
        {
            if (obj is Demotivator)
                client.DeleteByQuery<Demotivator>(del => del
                                                        .Query(q => q
                                                        .QueryString(qs => qs
                                                        .Query((obj as Demotivator).DemotivatorName)
                                                        .OnFields(f => f.DemotivatorName))));
            client.Refresh();
        }
        public List<Demotivator> SearchByDemotivatorName(string term)
        {
            var result = client.Search<Demotivator>(s => s
                   .Index("2_index")
                   .Query(q => q.QueryString(qs => qs.Query(term+"*")
                   .OnFields(f => f.DemotivatorName))));
            return result.Hits.Select(t => t.Source).ToList();
        }
        public List<ApplicationUser> SearchByUserName(string term)
        {
            var result = client.Search<ApplicationUser>(s => s
                     .Index("2_index")
                     .Query(q => q.QueryString(qs => qs.Query(term + "*")
                     .OnFields(f => f.UserName))));
            return result.Hits.Select(t => t.Source).ToList();
        }
    }
}