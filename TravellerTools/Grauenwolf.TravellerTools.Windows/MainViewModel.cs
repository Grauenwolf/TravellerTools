using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Tortuga.Sails;

namespace Grauenwolf.TravellerTools.Windows
{
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {

        }

        ICommand DownloadUniverseCommand { get { return GetCommand(DownloadUniverse); } }


        void DownloadUniverse()
        {
            var dlg = new SaveFileDialog();
            dlg.DefaultExt = "tt-universe";
            dlg.Filter = "Traveller Universe|universe.tt-universe";
            dlg.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Traveller Tools");
            Directory.CreateDirectory(dlg.InitialDirectory);

            var result = dlg.ShowDialog();

            if (result == true)
            {



            }
        }

    }
}
