using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 20;
    [SerializeField] private float _dmg = 20;

    public float Dmg() 
    { return _dmg; }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 7) Destroy(this.gameObject);
    }
}
