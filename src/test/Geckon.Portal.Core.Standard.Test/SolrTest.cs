using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Geckon.Portal.Core.Index;
using Geckon.Portal.Core.Standard.Index;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data;

namespace Geckon.Portal.Core.Standard.Test
{
    [TestFixture]
    public class SolrTest : SolrBaseTest
    {
        [Test]
        public void Should_Add_To_SolrIndex()
        {
            Solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );
        }

        [Test]
        public void Should_Get_All_From_SolrIndex()
        {
            Solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );
            Solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );
            Solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );
            Solr.Set( new DemoIndexItem( Guid.NewGuid(), DateTime.Now ) );

            SolrQuery query = new SolrQuery();
            query.Init( "*:*" );

            IEnumerable<IResult> results = Solr.Get( query );

            Assert.AreEqual( 4, results.Count() );
            
            foreach( GuidResult guid in results )
            {
                Assert.AreNotEqual( Guid.Empty, guid.Guid );
            }
        }

        [Test]
        public void Should_Create_Document_From_IIndexable()
        {
            Guid     guid = Guid.Parse( "02f0174c-a7e0-4e80-aeef-ceb18e28e2b7" );
            DateTime date = DateTime.Parse( "26-10-2011 17:59:49" );

            string document = Solr.ConvertToSolrDocument( new DemoIndexItem( guid, date ) ).ToString( System.Xml.Linq.SaveOptions.DisableFormatting );

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
