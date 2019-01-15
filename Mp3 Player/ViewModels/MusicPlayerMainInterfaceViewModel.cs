using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Linq;
using System;

namespace Mp3_Player
{

    public class MusicPlayerMainInterfaceViewModel : BaseViewModel
    {
        public MusicPlayerMainInterfaceViewModel()
        {
            OpenDialogCommand = new RelayCommand(OpenDialogWindow);
            PlayAudioCommand = new RelayCommand(PlayAudio);
            openFileDialog = new OpenFileDialog();
            mediaPlayer = new MediaPlayer();
        }

        #region Commands
        public ICommand OpenDialogCommand { get; set; }
        public ICommand PlayAudioCommand { get; set; }
        #endregion

        #region Public Properties
        public double Width { get; set; } = 380;
        public double Height { get; set; } = 340;
        public string FileName { get; set; }
        public string Title { get; set; }
        #endregion

        #region Private Members
        OpenFileDialog openFileDialog;
        MediaPlayer mediaPlayer;
        #endregion

        #region Command Methods
        private void OpenDialogWindow()
        {
            try
            {
                openFileDialog.ShowDialog();
                FileName = openFileDialog.FileName;
                Title = FileName.Split(' ').Last();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            
        }
        
        private void PlayAudio()
        {
            
        }
        #endregion
    }
}
