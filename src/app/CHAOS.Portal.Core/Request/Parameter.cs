namespace CHAOS.Portal.Core.Request
{
    public class Parameter
    {
        #region Properties

        public string Name { get; set; }
        public string Value { get; set; }

        #endregion
        #region Constructors

        public Parameter(string name, string value)
        {
            Name  = name;
            Value = value;
        }

        #endregion
    }
}
