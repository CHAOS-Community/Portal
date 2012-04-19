using System.Data.Objects;
using System.Linq;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.Core.Test;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using CHAOS.Portal.Extensions.Group;
using Geckon;
using NUnit.Framework;

namespace CHAOS.Portal.Extensions.Test
{
    [TestFixture]
    public class GroupTest : TestBase
    {
        private GroupExtension Extension { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            PortalApplication.LoadedExtensions.Add( "Group", new GroupExtension() );

            Extension = new GroupExtension();
        }

        [Test]
        public void Should_Get_Group()
        {
            Extension.Get( AdminCallContext, null );
            
           Assert.AreEqual( AdminGroup.GUID.ToByteArray(), (AdminCallContext.PortalResponse.PortalResult.GetModule("Geckon.Portal").Results[0] as CHAOS.Portal.DTO.Standard.Group).GUID.ToByteArray() );
        }

        [Test]
        public void Should_Create_Group()
        {
            Extension.Create( AdminCallContext, "my group", 0 );
            
            Assert.AreEqual( "my group", (AdminCallContext.PortalResponse.PortalResult.GetModule("Geckon.Portal").Results[0] as CHAOS.Portal.DTO.Standard.Group).Name );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Create_Group()
        {
            Extension.Create(AnonCallContext, "InsufficientPermissionsException", 0);
        }

        [Test]
        public void Should_Delete_Group()
        {
            Extension.Delete( AdminCallContext, AdminGroup.GUID );

            Assert.AreEqual( 1, (AdminCallContext.PortalResponse.PortalResult.GetModule("Geckon.Portal").Results[0] as CHAOS.Portal.DTO.Standard.ScalarResult).Value );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group()
        {
            Extension.Delete( AnonCallContext, AdminGroup.GUID );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Group2()
        {
            using( PortalEntities db = new PortalEntities( ) )
            {
                UUID            guid = new UUID();
                ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

                db.Group_Create( guid.ToByteArray(), "no permission",UserAdministrator.GUID.ToByteArray(), 0x00, errorCode );

                Extension.Delete( AnonCallContext, guid );
            }            
        }

        [Test]
        public void Should_Update_Group()
        {
            Extension.Update( AdminCallContext, AdminGroup.GUID, "success", 0 );

            using( PortalEntities db = new PortalEntities( ) )
            {
                Data.EF.Group group = db.Group_Get( AdminGroup.GUID.ToByteArray(), null, UserAdministrator.GUID.ToByteArray() ).First();

                Assert.AreEqual("success", group.Name );
            }
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Group()
        {
            Extension.Update( AnonCallContext, AdminGroup.GUID, "hj", (int) AdminGroup.SystemPermission );
        }
    }
}
