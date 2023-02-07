using System.Threading.Tasks;

namespace ViewModels.Commands
{
    /// <summary>
    /// <see cref="AsyncCommandBase"/> class is async version of <see cref="CommandBase"/> class
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool isExecuting;
        public bool IsExecuting
        {
            get
            {
                return isExecuting;
            }
            set
            {
                isExecuting = value;
                onCanExecuteChanged();
            }
        }
        /// <inheritdoc />
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        /// <inheritdoc />
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        /// <summary>
        /// Defines the task to be called when the command is invoked.
        /// </summary>
        public abstract Task ExecuteAsync(object? parameter);
    }
}
