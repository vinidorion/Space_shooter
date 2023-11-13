using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _life = 50;
    [SerializeField] private GameObject[] _listePU = default;
    [SerializeField] private GameObject _explosion = default;
    [SerializeField] private AudioClip _boom_sound = default;

    private void Start()
    {

    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -4) transform.position = new Vector3(Random.Range(-9, 9), 6, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _life = _life - Laser.Instance.Dmg();
            Destroy(other.gameObject);
            if (_life <= 0 ) Destroy(gameObject);
            UIManager.Instance.AjouterScore(10);
            PowerSpawn();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_boom_sound, Camera.main.transform.position, 4f);
        }
        else if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Player.Instance.DmgPlayer(1);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_boom_sound, Camera.main.transform.position, 4f);
        }
    }

    private void PowerSpawn()
    {
        int _PU = Random.Range(0, _listePU.Length * 3);
        if (_PU < _listePU.Length) Instantiate(_listePU[_PU], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
}
