using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnScript : MonoBehaviour {

    public int noOfLanes;

    public GameObject Car;

    public float ySpawn;
    private float zSpawn;
    private Quaternion rotationSpawn;

    public float[] rangeWaitTime;

    private float[] xValues;
    private float screenWidth;

    private bool shouldSpawn;

	// Use this for initialization
	void Start () {
		
        if (noOfLanes == 3)
        {

            screenWidth = 2 * Camera.main.orthographicSize * Screen.width / Screen.height;
            xValues = new float[3];

            xValues[0] = - Camera.main.orthographicSize * Screen.width / Screen.height + Camera.main.orthographicSize * Screen.width / (3 * Screen.height);
            xValues[1] = - Camera.main.orthographicSize * Screen.width / Screen.height + Camera.main.orthographicSize * Screen.width / Screen.height;
            xValues[2] = -Camera.main.orthographicSize * Screen.width / Screen.height + (5 * Camera.main.orthographicSize * Screen.width) / (3 * Screen.height);

        }

        shouldSpawn = true;

        zSpawn = Car.GetComponent<Transform>().position.z;
        rotationSpawn = Car.GetComponent<Transform>().rotation;

	}
	
	// Update is called once per frame
	void Update () {

        if (shouldSpawn)
        {
            shouldSpawn = false;
            StartCoroutine(SpawnCar());
        }
		
	}

    IEnumerator SpawnCar()
    {

        yield return new WaitForSeconds(Random.Range(rangeWaitTime[0], rangeWaitTime[1]));

        int index = Random.Range(0, 3);

        GameObject spawnedCar = Instantiate(
            Car,
            new Vector3(xValues[index], ySpawn, zSpawn),
            rotationSpawn
        );

        shouldSpawn = true;

    }

}
