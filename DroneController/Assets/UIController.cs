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
        Rect bounds;

        Vector2 resting;
        Vector3 touchPos;

        void Start()
        {
            //get start resting position
            print(parent);
            resting = transform.position;

            bounds = new Rect();

            bounds.xMax = (float)Math.Floor(parent.transform.localScale.x) / 2 + parent.transform.position.x;
            bounds.xMin = parent.transform.position.x - (float)Math.Floor(parent.transform.localScale.x / 2);
            bounds.yMax = parent.transform.localScale.y / 2 + parent.transform.position.y;
            bounds.yMin = parent.transform.position.y - (float)Math.Floor(parent.transform.localScale.y / 2);
        }

        // Update is called once per frame
        void Update()
        {
            //get Touch move along constraints
            if (Input.touchCount > 0)
            {
                Touch input = Input.GetTouch(0);

                //check
                if (!this.TouchInBounds(input)) { return; }
                
                switch (input.phase)
                {
                    case TouchPhase.Began:
                        touchPos = Camera.main.ScreenToWorldPoint(new Vector3(input.position.x, input.position.y, 10));
                        print("started interaction");
                    case TouchPhase.Moved:
                        //place ui element into new spot, write value
                        Vector2 offset = touchPos - Camera.main.ScreenToWorldPoint(new Vector3(input.position.x, input.position.y, 10));
                        this.UpdateUI(offset);
                        print(offset);
                        break;

                    case TouchPhase.Stationary:
                        return;

                    case TouchPhase.Canceled:
                    case TouchPhase.Ended:
                        if (clip)
                        {
                            transform.position = resting;
                        }
                        break;
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
            Vector3 minTouch = touch.position + new Vector2(touch.radius + touch.radiusVariance, touch.radius + touch.radiusVariance); minTouch.z = 10;
            Vector3 maxTouch = touch.position - new Vector2(touch.radius + touch.radiusVariance, touch.radius + touch.radiusVariance); maxTouch.z = 10;

            Rect touchBounds = new Rect();
            touchBounds.min = Camera.main.ScreenToWorldPoint(minTouch); touchBounds.max = Camera.main.ScreenToWorldPoint(maxTouch);


            Rect UIBounds = new Rect(transform.position - new Vector3(transform.localScale.x / 2, transform.localScale.y / 2, 0), transform.localScale);

            return UIBounds.Overlaps(touchBounds);
        }

        void UpdateUI(Vector2 newPos){
            //make sure it doesn't exceed the bounds
            //if locked in y or if both arent ticked
            //rewrite Update ui
            Vector2 finalPos = Vector2.zero;

            //y
            if(!xConstraint && Math.Max(newPos.y, bounds.yMax) != newPos.y && Math.Min(newPos.y, bounds.yMin) != newPos.y)
            {
                finalPos = new Vector2(transform.position.x, newPos.y);
            }
            else if(Math.Max(newPos.y, bounds.yMax) == newPos.y) 
            {
                finalPos = new Vector2(transform.position.x, bounds.yMax);
            }
            else if(Math.Min(newPos.y, bounds.yMin) == newPos.y)
            {
                finalPos = new Vector2(transform.position.x, bounds.yMin);
            }
            
            
            //x
            if(!yConstraint && Math.Max(newPos.x, bounds.xMax) != newPos.x && Math.Min(newPos.x, bounds.xMin) != newPos.x)
            {
                finalPos = new Vector2(newPos.x, finalPos.y);
            }
            else if(Math.Max(newPos.x, bounds.xMax) == newPos.x) 
            {
                finalPos = new Vector2(bounds.xMax, finalPos.y);
            }
            else if(Math.Min(newPos.x, bounds.xMin) == newPos.x)
            {
                finalPos = new Vector2(bounds.xMin, finalPos.y);
            }

            print("updated");
            transform.position = finalPos;

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
