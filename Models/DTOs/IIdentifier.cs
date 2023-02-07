using System;

namespace Models.Models
{
    /// <summary>
    /// This interface is a to make sure all classes and object data transfers have Id (for generic purposes) 
    /// </summary>
    public interface IIdentifier
    {
        public Guid Id { get; set; }
    }
}
