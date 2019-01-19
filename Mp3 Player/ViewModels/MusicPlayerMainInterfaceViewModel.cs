using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Mp3_Player
{

    public class MusicPlayerMainInterfaceViewModel : BaseViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public MusicPlayerMainInterfaceViewModel()
        {
            //Command to open a folder explorer
            OpenDialogCommand = new RelayCommand(OpenDialogWindow);

            //Command to play a music
            PlayAudioCommand = new RelayCommand(PlayAudio);

            //Command to pause a music
            PauseAudioCommand = new RelayCommand(PauseAudio);

            //Command to open multiple files
            SelectMultipleFileCommand = new RelayCommand(OpenFiles);

            PlayNextSongCommand = new RelayCommand(PlayNext);

            PlayPreviousSongCommand = new RelayCommand(PlayPrevious);

            //Create a OpenDialogFIle class instance
            openFileDialog = new OpenFileDialog();

            //Create a MediaPlayer class instance
            mediaPlayer = new MediaPlayer();
        }

        #region Commands
        /// <summary>
        /// Command to open a File explorer
        /// </summary>
        public ICommand OpenDialogCommand { get; set; }

        /// <summary>
        /// Command to play audio
        /// </summary>
        public ICommand PlayAudioCommand { get; set; }

        /// <summary>
        /// Command to pause audio
        /// </summary>
        public ICommand PauseAudioCommand { get; set; }

        /// <summary>
        /// Command to select multiple files to play
        /// </summary>
        public ICommand SelectMultipleFileCommand { get; set; }

        /// <summary>
        /// Command to play next song from File Names array
        /// </summary>
        public ICommand PlayNextSongCommand { get; set; }

        /// <summary>
        /// Command to play previous song from File Names array
        /// </summary>
        public ICommand PlayPreviousSongCommand { get; set; }
        #endregion

        #region Public Properties
        /// <summary>
        /// Width of the control
        /// </summary>
        public double Width { get; set; } = 380;

        /// <summary>
        /// Height of the control
        /// </summary>
        public double Height { get; set; } = 340;

        /// <summary>
        /// File name of the selected mp3 format file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Title of the music to display
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// True if the Play button should be visible
        /// </summary>
        public bool PlayButtonVisible { get; set; } = true;

        /// <summary>
        /// True if the pause button should be visible
        /// </summary>
        public bool PauseButtonVisible { get; set; } = false;

        private bool OneSong = false;
        private bool MultipleSongs = false;

        private int songIndex = 0;
        private int songTitleIndex = 0;

        private string[] FileTitles;

        public List<string> FileNames;
        #endregion

        #region Private Members
        /// <summary>
        /// OpenFileDialog object to use for select file
        /// </summary>
        OpenFileDialog openFileDialog;

        /// <summary>
        /// MediaPlayer object to play or pause music
        /// </summary>
        MediaPlayer mediaPlayer;
        #endregion

        #region Command Methods
        /// <summary>
        /// Method to open file explorer 
        /// </summary>
        private void OpenDialogWindow()
        {
            MultipleSongs = false;
            OneSong = true;
            try
            {
                openFileDialog.ShowDialog();
                FileName = openFileDialog.FileName;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            
        }

        /// <summary>
        /// Method to play audio
        /// </summary>
        private void Play(string path)
        { 
            mediaPlayer.Open(new Uri(path));
            Title = openFileDialog.SafeFileName;
            mediaPlayer.Play();
            PlayButtonVisible = false;
            PauseButtonVisible = true;
        }

        private void Play(string path, int titleIndex)
        {
            FileTitles = openFileDialog.SafeFileNames;

            mediaPlayer.Open(new Uri(path));
            Title = FileTitles[titleIndex];
            mediaPlayer.Play();
            PlayButtonVisible = false;
            PauseButtonVisible = true;
        }

        private void PlayAudio()
        {
            if (MultipleSongs)
                Play(FileNames[songIndex], songTitleIndex);
            else
                Play(FileName);
        }

        /// <summary>
        /// Method to pause audio
        /// </summary>
        private void PauseAudio()
        {
            mediaPlayer.Pause();
            PlayButtonVisible = true;
            PauseButtonVisible = false;
        }

        /// <summary>
        /// Method to open multiple files
        /// </summary>
        private void OpenFiles()
        {
            OneSong = false;
            MultipleSongs = true;
            try
            {
                openFileDialog.Multiselect = true;
                openFileDialog.ShowDialog();
                FileNames = openFileDialog.FileNames.ToList();      
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }

        }

        /// <summary>
        /// Method to play next song from the song list
        /// </summary>
        private void PlayNext()
        {
            if (songIndex > FileNames.Count && songIndex < 0
                && songTitleIndex > FileNames.Count && songTitleIndex < 0)
                return;
            else
               Play(FileNames[songIndex + 1], songTitleIndex + 1);
               songIndex++;
               songTitleIndex++;
            
        }

        /// <summary>
        /// Method to play previous song from the song list
        /// </summary>
        private void PlayPrevious()
        {
            if (songIndex > FileNames.Count && songIndex < 0
                   && songTitleIndex > FileNames.Count && songTitleIndex < 0)
                return;
            else
                Play(FileNames[songIndex - 1], songTitleIndex - 1);
                songIndex--;
                songTitleIndex--;
            
            
        }
        #endregion
    }
}
