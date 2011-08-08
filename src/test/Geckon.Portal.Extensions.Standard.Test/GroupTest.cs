using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class GroupTest : TestBase
    {
        [Test]
        public void Should_Get_Group()
        {
            GroupExtension extension = new GroupExtension( new PortalContextMock() );
            extension.Init( new Result() );

            XDocument xdoc = XDocument.Parse( extension.Get( AdminUser.SessionID.ToString() ).Content );
            
            Assert.AreEqual( AdminGroup.GUID.ToString(), xdoc.Descendants("GUID").First().Value );
        }

        [Test]
        public void Should_Create_Group()
        {
            GroupExtension extension = new GroupExtension( new PortalContextMock() );
            extension.Init( new Result() );

            XDocument xdoc = XDocument.Parse( extension.Create( AdminUser.SessionID.ToString(), "my group" ).Content );
            
            Assert.AreEqual( "my group", xdoc.Descendants("Name").First().Value );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Create_Group()
        {
            GroupExtension extension = new GroupExtension( new PortalContextMock( ) );
            extension.Init( new Result() );

            extension.Create( User.SessionID.ToString(), "InsufficientPermissionsExcention" );
        }

        [Test]
        public void Should_Delete_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse( extension.Delete( AdminUser.SessionID.ToString(), AdminGroup.GUID.ToString() ).Content );

            Assert.AreEqual("1", xdoc.Descendants("Value").First().Value);
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse( extension.Delete( User.SessionID.ToString(), AdminGroup.GUID.ToString() ).Content);
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group2()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                Data.Group group = db.Group_Insert( null, "no permission" ).First();

                extension.Delete( User.SessionID.ToString(), group.GUID.ToString() );
            }            
        }

        [Test]
        public void Should_Update_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse( extension.Update( AdminUser.SessionID.ToString(), AdminGroup.GUID.ToString(), "success" ).Content );
            
            Assert.AreEqual( "success", xdoc.Descendants( "Name" ).First().Value );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            extension.Update(User.SessionID.ToString(), AdminGroup.GUID.ToString(), "hj");
        }
    }
}
