namespace Chaos.Portal.Data.Dto
{
    using System;
    using CHAOS;
    using CHAOS.Serialization;

    /// <summary>
    /// The ClientSettings interface.
    /// </summary>
    public interface IClientSettings : IResult
    {
        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        [Serialize]
        UUID GUID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Serialize]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the client Settings.
        /// </summary>
        [Serialize]
        string Settings { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [Serialize]
        DateTime DateCreated { get; set; }
    }
}