using Acr.UserDialogs;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Helpers
{
   public class ConnectivityChecker
    {

        public ConnectivityChecker()
        {
            connectedToInternet();
        }
        public void connectedToInternet()
        {
            var dialog = UserDialogs.Instance;
            if (!CrossConnectivity.IsSupported)
            {
             
            }

            //Do this only if you need to and aren't listening to any other events as they will not fire.
            var connectivity = CrossConnectivity.Current;

            try
            {
                if (!connectivity.IsConnected)
                {
                    dialog.AlertAsync("No internet connection detected", "Connection error!");
                }
            }
            finally
            {
                CrossConnectivity.Dispose();
            }

        }
    }
}
