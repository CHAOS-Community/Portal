using CHAOS.Serialization;

namespace CHAOS.Portal.DTO
{
    [Serialize("Result")]
    public interface IResult
    {
        string Fullname { get; }
    }
}
