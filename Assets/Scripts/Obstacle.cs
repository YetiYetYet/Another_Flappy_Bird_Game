using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager _gameManager;
    public float speed = 1f;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager") == null) return;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        transform.position +=  speed * Time.deltaTime * Vector3.left;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _gameManager.AddScore();
    }
}
