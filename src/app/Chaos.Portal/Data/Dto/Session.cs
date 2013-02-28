namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;

    public class Session : AResult
	{
		#region Properties

        public string DocumentID 
        { 
            get
            {
                return Guid.ToString();
            }
            set
            {
                Guid = new Guid(value);
            }
        }

        [Serialize]
		public Guid Guid { get; set; }
        [Serialize]
        public Guid UserGuid { get; set; }
        [Serialize]
        public DateTime DateCreated { get; set; }
        [Serialize]
        public DateTime? DateModified { get; set; }

		#endregion
	}
}
