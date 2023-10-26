using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _life = 50;

    private Laser _laser;


    private void Start()
    {
        //_player = FindObjectOfType<Player>;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -4) transform.position = new Vector3(Random.Range(-9, 9), 6, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Laser")
        {
            //_life = _life - other.Dmg();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
