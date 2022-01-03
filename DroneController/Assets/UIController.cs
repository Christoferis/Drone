using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Controller {

    public class UIController : MonoBehaviour
    {
        //a script that controls the position of the moving ui element
        // Start is called before the first frame update
        //xConstraint: locked to x axis, yConstraint locked to y axis
        public bool xConstraint;
        public bool yConstraint;
        public bool clip;
        public string type;
        public GameObject parent;

        //describe max bounds
        Vector2 max;
        Vector2 min;
        Vector2 resting;
        Vector3 touchPos;

        void Start()
        {
            //get start resting position
            resting = transform.position;

            //since everything is centered and at y = 0
            max.x = (float)Math.Floor(parent.transform.localScale.x / 2 + parent.transform.position.x);
            max.y = parent.transform.localScale.y / 2 + parent.transform.position.y;

            min.x = (float)Math.Floor(parent.transform.localScale.x / 2 - parent.transform.position.x);
            min.y = parent.transform.localScale.y / 2 - parent.transform.position.y;
        }

        // Update is called once per frame
        void Update()
        {
            //get Touch move along constraints
            if (Input.touchCount > 0)
            {
                Touch input = Input.GetTouch(0);

                //check if in bounds
                Vector2 i_min = new(input.position.x - input.radius + input.radiusVariance, input.position.y - input.radius + input.radiusVariance);
                Vector2 i_max = new(input.position.x + input.radius + input.radiusVariance, input.position.y + input.radius + input.radiusVariance);


                switch (input.phase)
                {
                    case TouchPhase.Began or TouchPhase.Stationary:
                        touchPos = Camera.main.ScreenToWorldPoint(input.position);
                        break;

                    case TouchPhase.Moved:
                        this.SendData(touchPos - Camera.main.ScreenToWorldPoint(input.position));
                        break;

                    case TouchPhase.Ended or TouchPhase.Canceled:
                        if (clip)
                        {
                            transform.position = resting;
                        }
                        break;

                }



                //get different cases


                ////create input bounds
                //Vector2 i_min = new(input.position.x - input.radius + input.radiusVariance, input.position.y - input.radius + input.radiusVariance);
                //Vector2 i_max = new(input.position.x + input.radius + input.radiusVariance, input.position.y + input.radius + input.radiusVariance);



                ////check range
                //if {}

            }




        }

        //send, convert data to Controller.cs
        void SendData(Vector2 data)
        {
            //change ui element
            if (xConstraint && !yConstraint)
            {
                Vector2 d = new Vector2(data.x, 0);
                transform.position += new Vector3(data.x, 0, 0);
            }

            if (yConstraint && !xConstraint)
            {
                Vector2 d = new Vector2(0, data.y);
                transform.position += new Vector3(0, data.y, 0);
            }

            if (!xConstraint && !yConstraint)
            {
                Vector2 d = data;
                transform.position += new Vector3(data.x, data.y, 0);
            }
            else
            {
                Vector2 d = Vector2.zero;
            }

            //TODO: Read about SendMessages

        }


    }
}
