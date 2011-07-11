namespace Geckon.Portal.Core
{
    public interface IChecked<T>
    {
        bool IsChecked { get; set; }
        T Value { get; set; }
    }
}