namespace Chaos.Portal.IntegrationTest.Data
{
    using System.Configuration;

    using Chaos.Deployment.UI.Console.Action.Database.Import;

    using NUnit.Framework;

    [TestFixture]
    public class PortalRepositoryTest
    {
        [SetUp]
        public void SetUp()
        {
            var importer = new ImportDeployment();

            importer.Parameters.ConnectionString = ConfigurationManager.ConnectionStrings["portal"].ConnectionString;
            importer.Parameters.Path = @"..\..\..\..\..\sql\6.data\initial.sql";

            importer.Run();

            importer.Parameters.Path = "integraion_tests_base_data.sql";

            importer.Run();
        }
    }
}