﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField] private float _speed = 3.0f;
    // _powerUpID  0=TripleShot   1=Speed    2=Shield
    [SerializeField] private int _powerUpID = default;
    //[SerializeField] private AudioClip _powerUpSound = default;

    // Update is called once per frame
    void Update()    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            Destroy(this.gameObject);
            //AudioSource.PlayClipAtPoint(_powerUpSound, Camera.main.transform.position, 0.6f);
            switch (_powerUpID) {
                    case 0:
                        Player.Instance.PUTripleShot();
                        Debug.Log("!");
                        break;
                    case 1:
                        Player.Instance.PUSpeed();
                        break;
                    case 2:
                        Player.Instance.PUShield();
                        break;
            }
            
        }
    }
}
