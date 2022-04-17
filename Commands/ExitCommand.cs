using System.Windows;

namespace LibraryExample.Commands
{
    public class ExitCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
