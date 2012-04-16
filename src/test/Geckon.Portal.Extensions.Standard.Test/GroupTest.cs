using System.Data.Objects;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Test;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class GroupTest : TestBase
    {
        [Test]
        public void Should_Get_Group()
        {
            GroupExtension extension = new GroupExtension(  );
            
            extension.Init( new PortalContextMock() );
            extension.Get( AdminCallContext, null );
            
            Assert.AreEqual( AdminGroup.GUID.ToString(), XDocument.Parse( extension.Result ).Descendants("GUID").First().Value );
        }

        [Test]
        public void Should_Create_Group()
        {
            GroupExtension extension = new GroupExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Create( AdminCallContext, "my group", 0 );
            
            Assert.AreEqual( "my group", XDocument.Parse( extension.Result ).Descendants("Name").First().Value );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Create_Group()
        {
            GroupExtension extension = new GroupExtension( );

            extension.Init( new PortalContextMock() );
            extension.Create( AnonCallContext, "InsufficientPermissionsExcention", 0 );
        }

        [Test]
        public void Should_Delete_Group()
        {
            GroupExtension extension = new GroupExtension();

            extension.Init( new PortalContextMock() );
            extension.Delete( AdminCallContext, AdminGroup.GUID.ToString() );

            Assert.AreEqual("1", XDocument.Parse( extension.Result ).Descendants("Value").First().Value);
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group()
        {
            GroupExtension extension = new GroupExtension();

            extension.Init( new PortalContextMock() );
            extension.Delete( AnonCallContext, AdminGroup.GUID.ToString() );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group2()
        {
            GroupExtension extension = new GroupExtension();
            extension.Init( new PortalContextMock() );

            using( PortalEntities db = new PortalEntities( ) )
            {
                UUID            guid = new UUID();
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

                db.Group_Create( guid.ToByteArray(), "no permission",UserAdministrator.GUID.ToByteArray(), 0x00, errorCode );

                extension.Delete( AnonCallContext, guid.ToString() );
            }            
        }

        [Test]
        public void Should_Update_Group()
        {
            GroupExtension extension = new GroupExtension();

            extension.Init( new PortalContextMock() );
            
            extension.Update( AdminCallContext, AdminGroup.GUID.ToString(), "success", 0 );

            using( PortalEntities db = new PortalEntities( ) )
            {
                Group group = db.Group_Get( AdminGroup.GUID.ToByteArray(), null, UserAdministrator.GUID.ToByteArray() ).First();

                Assert.AreEqual("success", group.Name );
            }
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Group()
        {
            GroupExtension extension = new GroupExtension();

            extension.Init( new PortalContextMock() );
            extension.Update( AnonCallContext, AdminGroup.GUID.ToString(), "hj", (int) AdminGroup.SystemPermission );
        }
    }
}
