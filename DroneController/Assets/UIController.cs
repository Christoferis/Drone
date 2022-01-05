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

            //since everything is centered and at y = 0 its a rectangle btw -> just convert to a rect
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

                //check
                if (this.TouchInBounds(input))
                {
                    switch (input.phase)
                    {
                        case TouchPhase.Stationary:
                        case TouchPhase.Began:
                            touchPos = Camera.main.ScreenToWorldPoint(input.position);
                            break;

                        case TouchPhase.Moved:
                            //place ui element into new spot, write value
                            Vector2 offset = touchPos - Camera.main.ScreenToWorldPoint(input.position);
                            this.UpdateUI(offset);
                            break;

                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            if (clip)
                            {
                                transform.position = resting;
                            }
                            break;
                    }
                }
            }
            //if nothing is touching the screen
            else if (Input.touchCount == 0)
            {
                if (clip)
                {
                    transform.position = resting;
                }
            }
        }

        bool TouchInBounds(Touch touch)
        {
            Rect touchBounds = new Rect(touch.position - new Vector2(touch.radius + touch.radiusVariance, touch.radius + touch.radiusVariance), new Vector2((float) Math.Pow(touch.radius + touch.radiusVariance, 2), (float) Math.Pow(touch.radius + touch.radiusVariance, 2)));

            Rect UIBounds = new Rect(transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), transform.localScale);

            return touchBounds.Overlaps(UIBounds);
        }

        void UpdateUI(Vector2 newPos){
            //make sure it doesn't exceed the bounds
            //if locked in y or if both arent ticked
            if(!xConstraint && transform.position.y + newPos.y <= max.y && transform.position.y + newPos.y >= min.y)
            {
                transform.position += new Vector3(0, newPos.y, 0);

            }
            else if (!xConstraint && transform.position.y + newPos.y > max.y)
            {
                transform.position = new Vector3(0, max.y, 0);   
            }
            else if(!xConstraint && transform.position.y + newPos.y < min.y)
            {
                transform.position = new Vector3(0, min.y, 0);
            }

            //if locked in x or if both arent ticked
            if(!yConstraint && transform.position.x + newPos.y <= max.x && transform.position.x + newPos.x >= min.x)
            {
                transform.position += new Vector3(newPos.x, 0, 0);
            }
            else if (!yConstraint && transform.position.x + newPos.x > max.x)
            {
                transform.position = new Vector3(max.x, 0, 0);   
            }
            else if(!yConstraint && transform.position.x + newPos.x < min.x)
            {
                transform.position = new Vector3(min.x, 0, 0);
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
