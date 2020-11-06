using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject obstacle;
    public float height;
    public float lifeTimeObstacle = 15f;
    public float obstacleSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            GameObject newObstacle = Instantiate(obstacle);
            newObstacle.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            newObstacle.GetComponent<Obstacle>().speed = obstacleSpeed;
            Destroy(newObstacle, lifeTimeObstacle);

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
