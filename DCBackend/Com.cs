using System;
using Android.Bluetooth;

//File there to be used as a dll to communicate using Androids Bluetooth
namespace DCBackend
{

    public class Com
    {

        bool connection;

        public Com()
        {
            //enable Bluetooth and search for device
        }

        public bool Connected()
        {
            return connection;
        }

    }

}
