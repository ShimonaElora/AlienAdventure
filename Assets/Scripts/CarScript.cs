using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour, InterfacePooledObject {

    public float speed;

    private ObjectPooler objectPooler;

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    private CarSpawnScript carSpawnScript;

    public int xIndex;

    private bool swipeStart;

    // Use this for initialization
    public void OnObjectSpawn () {

        GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0, speed * 100)
            );

        objectPooler = GameObject.Find("Object Pooler").GetComponent<ObjectPooler>();

        carSpawnScript = GameObject.Find("Cars").GetComponent<CarSpawnScript>(); 
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            //We transform the touch position into word space from screen space and store it.
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

            //We now raycast with this information. If we have hit something we can process it.
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            Touch touch = Input.touches[0];


            if (hitInformation.collider != null)
            {
                //We should have hit something with a 2D Physics collider!
                GameObject touchedObject = hitInformation.transform.gameObject;
                //touchedObject should be the object someone touched.
                Behaviour halo = (Behaviour)touchedObject.GetComponent("Halo");
                halo.enabled = true;

                if (touchPhase == TouchPhase.Began)
                {
                    startPos = touch.position;
                    swipeStart = true;
                }

            }
            
            if (swipeStart && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)) {

                float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

                if (swipeDistHorizontal > minSwipeDistX)

                {

                    float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                    if (swipeValue > 0) //right
                    {

                        if (xIndex != carSpawnScript.xValues.Length - 1)
                        {

                            GetComponent<Transform>().position = new Vector3(carSpawnScript.xValues[++xIndex], transform.position.y, transform.position.z);

                        }

                    }

                    else if (swipeValue < 0) //left
                    {

                        if (xIndex != 0)
                        {

                            GetComponent<Transform>().position = new Vector3(carSpawnScript.xValues[--xIndex], transform.position.y, transform.position.z);

                        }

                    }

                }

                swipeStart = false;

            }

        }
        else
        {
            Behaviour halo = (Behaviour)GetComponent("Halo");
            halo.enabled = false;
        }

        if (GetComponent<Transform>().position.y <= - 8)
        {
            objectPooler.EnqueueToPool(StringConstants.cars, gameObject);
        }

	}
}
