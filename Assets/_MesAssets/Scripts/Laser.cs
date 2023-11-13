using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public static Laser Instance;

    [SerializeField] private float _speed = 20;
    [SerializeField] private int _dmg = 20;

    private void Awake()
    {
        Instance = this;
    }

    public int Dmg()
    { return _dmg; }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 7)
        {
            if (transform.parent == null) Destroy(this.gameObject);
            else Destroy(transform.parent.gameObject);
        }
    }
}
