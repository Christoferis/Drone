using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIController : MonoBehaviour
{
    //a script that controls the position of the moving ui element
    // Start is called before the first frame update
    public bool xConstraint;
    public bool yConstraint;
    public bool clip;
    public GameObject parent;

    //describe max bounds
    Vector2 max;
    Vector2 min;
    Vector2 resting;

    void Start()
    {
        //get start resting position
        resting = transform.position;

        //since everything is centered and at y = 0
        max.x = (float) Math.Floor(parent.transform.localScale.x / 2 + parent.transform.position.x);
        max.y = parent.transform.localScale.y / 2 + parent.transform.position.y;

        min.x = (float)Math.Floor(parent.transform.localScale.x / 2 - parent.transform.position.x);
        min.y = parent.transform.localScale.y / 2 - parent.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //get Touch move along constraints
        if(Input.touchCount > 0)
        {
            Touch input = Input.GetTouch(0);


            switch (input.phase)
            {

                case TouchPhase.Began:
                    break;

                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:

                    if (clip)
                    {

                    }
                    break;
            }



            //get different cases


            ////create input bounds
            //Vector2 i_min = new(input.position.x - input.radius + input.radiusVariance, input.position.y - input.radius + input.radiusVariance);
            //Vector2 i_max = new(input.position.x + input.radius + input.radiusVariance, input.position.y + input.radius + input.radiusVariance);

            ////also flip to fit the collision thingy
            //Rect i_bounds = new(new Vector2(i_min.x, i_min.y - i_max.y - i_min.y), i_max - i_min);


            ////check range
            //if {}

        }



        if (clip)
        {
            transform.position = resting;
        }
    }

    //send data to Controller.cs
    void SendData()
    {

    }

    
}
