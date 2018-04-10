using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0, speed * 100)
            );
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (GetComponent<Transform>().position.y <= - 8)
        {
            Destroy(gameObject);
        }

	}
}
