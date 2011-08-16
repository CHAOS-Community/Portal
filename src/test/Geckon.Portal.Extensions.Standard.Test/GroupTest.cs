using System;
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

            XDocument xdoc = XDocument.Parse( extension.Get( AdminUser.SessionID.ToString(), null ).Content );
            
            Assert.AreEqual( AdminGroup.GUID.ToString(), xdoc.Descendants("GUID").First().Value );
        }

        [Test]
        public void Should_Create_Group()
        {
            GroupExtension extension = new GroupExtension( new PortalContextMock() );
            extension.Init( new Result() );

            XDocument xdoc = XDocument.Parse( extension.Create( AdminUser.SessionID.ToString(), "my group", 0 ).Content );
            
            Assert.Greater( int.Parse( xdoc.Descendants("Value").First().Value ), 0 );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Create_Group()
        {
            GroupExtension extension = new GroupExtension( new PortalContextMock( ) );
            extension.Init( new Result() );

            extension.Create( User.SessionID.ToString(), "InsufficientPermissionsExcention", 0 );
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
                Guid guid = Guid.NewGuid();
                int result = db.Group_Insert( guid, "no permission", 0, AdminUser.ID );

                extension.Delete( User.SessionID.ToString(), guid.ToString() );
            }            
        }

        [Test]
        public void Should_Update_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xDoc = XDocument.Parse( extension.Update( AdminUser.SessionID.ToString(), AdminGroup.GUID.ToString(), "success", 0 ).Content ) ;

            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                Group group = db.Group_Get(int.Parse(xDoc.Descendants("Value").First().Value), null, null, AdminUser.ID).First();

                Assert.AreEqual("success", group.Name );
            }
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsExcention )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Group()
        {
            GroupExtension extension = new GroupExtension(new PortalContextMock());
            extension.Init(new Result());

            extension.Update(User.SessionID.ToString(), AdminGroup.GUID.ToString(), "hj", BitConverter.ToInt32( AdminGroup.SystemPermission.ToArray(), 0 ) );
        }
    }
}
