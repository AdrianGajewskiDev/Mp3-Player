using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mp3_Player
{
    ///<summary>
    ///A window view model that contains all needed properties
    ///<summary>
    public class WindowViewModel : BaseViewModel
    { 
        #region Constructor
        ///<summary>
        ///Default constructor
        ///<summary>
        public WindowViewModel(Window window)
        {
            mWindow = window;
            MinimizeCommand = new RelayCommand(Minimize);
            CloseCommand = new RelayCommand(Close);
        }
        #endregion
        #region Public Properties    
        public Thickness OuterMarginSize { get; set; } = new Thickness(20);
        public CornerRadius CornerRadius { get; set; } = new CornerRadius(20);
        #endregion

        #region Private Members
        private  Window mWindow;
        #endregion

        #region Commands
        public ICommand MinimizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        #endregion

        #region Commands Methods
        private void Minimize()
        {
            mWindow.WindowState = WindowState.Minimized;
        }
        private void Close()
        {
            mWindow.Close();
        }
        #endregion
    }
}
