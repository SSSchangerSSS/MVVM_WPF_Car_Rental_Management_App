namespace ViewModels.Commands.MainMenuCommands
{
    /// <summary>
    /// <see cref="MainMenuExitCommand"/> class is a command for exiting the app
    /// </summary>
    public class MainMenuExitCommand : CommandBase
    {
        ///<inheritdoc/>
        public override void Execute(object? parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
