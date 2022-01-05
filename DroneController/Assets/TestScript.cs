using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    SpriteRenderer g;
    // Start is called before the first frame update
    void Start()
    {
        print("Running...");

        g = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0) { return; }

        Touch input = Input.GetTouch(0);

        g.color = Color.red;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(input.position.x, input.position.y, 10));

        print(transform.position);

        print(input.position);
    }
}
