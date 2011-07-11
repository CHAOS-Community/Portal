namespace Geckon.Portal.Core
{
    public class Checked<T> : IChecked<T>
    {
        #region Properties

        public bool IsChecked { get; set; }

        public T Value { get; set; }

        #endregion
        #region Construction

        public Checked( T value )
        {
            Value     = value;
            IsChecked = false;
        }

        #endregion
    }
}
