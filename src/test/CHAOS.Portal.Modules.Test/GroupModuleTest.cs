using System.Configuration;
using System.Linq;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class GroupModuleTest : TestBase
    {
        #region Constructors

        private GroupModule GroupModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            GroupModule = new GroupModule();
            GroupModule.Initialize( ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString );
        }

        #endregion

        [Test]
        public void Should_Create_Group()
        {
            var group = GroupModule.Create( AdminCallContext, "My Group", 0 );

            Assert.AreEqual( "My Group", group.Name );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException )) ]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Create_Group()
        {
            GroupModule.Create( AnonCallContext, "InsufficientPermissionsException", 0 );
        }

        [Test]
        public void Should_Get_A_Group()
        {
            var group = GroupModule.Get( AdminCallContext, null );

            Assert.AreEqual( AdminGroup.GUID.ToByteArray(), group.GUID.ToByteArray() );
        }

        [Test]
        public void Should_Update_A_Group()
        {
            GroupModule.Update( AdminCallContext, AdminGroup.GUID, "success", null );

            using( var db = new PortalEntities( ) )
            {
                Group group = db.Group_Get( AdminGroup.GUID.ToByteArray(), null, UserAdministrator.GUID.ToByteArray() ).First();

                Assert.AreEqual("success", group.Name );
            }
        }

        [Test]
        public void Should_Delete_A_Group()
        {
            var result = GroupModule.Delete( AdminCallContext, AdminGroup.GUID );

            Assert.AreEqual( 1, result.Value );
        }
    }
}
