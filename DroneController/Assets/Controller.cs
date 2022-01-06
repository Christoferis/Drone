using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

//main script to control everything (Start Elements, Thread for coms etc.)
namespace Controller {

    public class Controller : MonoBehaviour
    {
        //Handle all things
        void Start()
        {
            //call thread from DC Backend dll


            //set lock Screen orientation to landscape
            Screen.autorotateToLandscapeLeft = true;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        // Update is called once per frame
        void Update()
        {
            //send packages to Bluetooth connection
        }

        void Send()
        {

        }

        //function called upon connection lost
        void OnLost()
        {

        }

        //collectionpoint for all info
        void AddtoPackage(string type, object data)
        {

        }
    }


}

