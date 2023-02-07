using System;
using System.Runtime.Serialization;

namespace Models.Exceptions
{
    /// <summary>
    /// generic exception that be thrown if an incoming model has conflict with an existing model
    /// </summary>
    public class GModelConflictException<T> : Exception 
    {
        public T IncomingModel { get; }
        public T ExistingModel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GModelConflictException{T}"/> class.
        /// </summary>
        /// <param name="incomingModel">Incoming model has conflict with an existing model.</param>
        /// <param name="existingModel">Existing model has conflict with an incoming model.</param>
        public GModelConflictException(T incomingModel, T existingModel)
        {
            IncomingModel = incomingModel;
            ExistingModel = existingModel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GModelConflictException{T}"/> class.
        /// </summary>
        /// <param name="incomingModel">Incoming model has conflict with an existing model.</param>
        /// <param name="existingModel">Existing Model has conflict with an incoming model.</param>
        /// <param name="message">Message is to be shown if exception happens.</param>
        public GModelConflictException(string? message , T incomingModel, T existingModel) : base(message)
        {
            IncomingModel = incomingModel;
            ExistingModel = existingModel;
        }
        public GModelConflictException(string? message, Exception? innerException , T incomingModel, T existingModel) : base(message, innerException)
        {
            IncomingModel = incomingModel;
            ExistingModel = existingModel;
        }

        protected GModelConflictException(SerializationInfo info, StreamingContext context , T incomingModel, T existingModel) : base(info, context)
        {
            IncomingModel = incomingModel;
            ExistingModel = existingModel;
        }
    }
}
