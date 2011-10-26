using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard.Test
{
    [TestFixture]
    public class SolrTest
    {
        [Test]
        public void Should_Add_To_SolrIndex()
        {
            Solr solr = new Solr();

            solr.AddCore( new SolrCoreConnection( "http://192.168.56.102:8080/solr/core0" ) );
            solr.AddCore( new SolrCoreConnection( "http://192.168.56.102:8080/solr/core1" ) );

            solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );
        }

        [Test]
        public void Should_Create_Document_From_IIndexable()
        {
            Guid     guid = Guid.Parse( "02f0174c-a7e0-4e80-aeef-ceb18e28e2b7" );
            DateTime date = DateTime.Parse( "26-10-2011 17:59:49" );

            string document = Solr.ConvertToSolrDocument( new DemoIndexItem( guid, date ) );

            Assert.AreEqual( "<doc><field name=\"guid\">02f0174c-a7e0-4e80-aeef-ceb18e28e2b7</field><field name=\"date\">2011-10-26T17:59:49Z</field></doc>", document );
        }
    }

    public class DemoIndexItem : IIndexable
    {
        public Guid Guid { get; set; }
        public DateTime Date { get; set; }

        public DemoIndexItem( Guid guid, DateTime date )
        {
            Guid = guid;
            Date = date;
        }

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return new KeyValuePair<string, string>( "guid", Guid.ToString() );
            yield return new KeyValuePair<string, string>( "date", Date.ToString( "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'" ));
        }
    }
}
