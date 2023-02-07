using System;
using System.Windows.Input;

namespace ViewModels.Commands
{
    /// <summary>
    /// <see cref="CommandBase"/> class is an abstract implementaion of <see cref="ICommand"/>
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        ///<inheritdoc/>
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        ///<inheritdoc/>
        public abstract void Execute(object? parameter);

        /// <summary>
        /// Will be called when <see cref="CanExecute"/> value changes
        /// </summary>
        public void onCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
