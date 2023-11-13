using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private GameObject _laser = default;
    [SerializeField] private GameObject _tripleLaser = default;
    [SerializeField] private GameObject _beam = default;
    private float _canShoot = -1;
    public static Player Instance; //Mise en place singleton
    private bool _tripled = false;
    private int _PUatkSpeedBoost = 1;
    private GameObject _shield = default;
    private Animator _animator;
    [SerializeField] private AudioClip _laser_sound = default;

    [Header("Stats")]
    [SerializeField] private float _speed = 12;
    [SerializeField] private int _life = 3;
    [SerializeField] private float _atkSpeed = 0.5f;
    private GameObject _atk = default;
    private bool _shielded = false;


    [Header("Limites")]
    [SerializeField] private float _maxX = 11;
    [SerializeField] private float _minX = -11;
    [SerializeField] private float _maxY = 5;
    [SerializeField] private float _minY = -3.5f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
        _atk = _laser;
        Laser.Instance = FindObjectOfType<Laser>();
        _life = 3;
        _shield = transform.GetChild(0).gameObject;
        _shield.SetActive(false);
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Mouvement();
        TirerLaser();
        ChangerLaser();
        GestionAnim();
    }

    private void GestionAnim()
    {
        if (Input.GetKeyDown(KeyCode.A)) _animator.SetBool("turning_Left", true);
        else if (Input.GetKeyUp(KeyCode.A)) _animator.SetBool("turning_Left", false);
        if (Input.GetKeyDown(KeyCode.D)) _animator.SetBool("turning_Right", true);
        else if (Input.GetKeyUp(KeyCode.D)) _animator.SetBool("turning_Right", false);
        if (_animator.GetBool("turning_Right")) _animator.SetBool("turning_Left", false);
    }

    private void ChangerLaser()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_atk == _laser)
            {
                _atk = _beam;
                _atkSpeed = 0.01f;
                Laser.Instance.SetSpeed(1000000000);
            }
            else if (_atk == _beam)
            {
                _atk = _laser;
                _atkSpeed = 0.5f;
                Laser.Instance.SetSpeed(20);
            }
        }
    }

    private void TirerLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canShoot)
        {
            if (!_tripled) Instantiate(_atk, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            else Instantiate(_tripleLaser, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            _canShoot = Time.time + _atkSpeed / _PUatkSpeedBoost;
            AudioSource.PlayClipAtPoint(_laser_sound, Camera.main.transform.position, 4f);
        }
        
    }

    private void Mouvement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xInput, yInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);

        //Limiter les positions
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _minY, _maxY), 0);
        if (transform.position.x > 11) transform.position = new Vector3(_minX, transform.position.y, 0);
        else if (transform.position.x < -11) transform.position = new Vector3(_maxX, transform.position.y, 0);

    }

    public void DmgPlayer(int dmg)
    {
        if (!_shield.activeSelf)
        {
            _life = _life - dmg;
            UIManager.Instance.ChangeLivesDisplayImage(_life);
            if (_life <= 0)
            {
                Destroy(this.gameObject);
                GestionJeu.Instance.EndGame();
            }
        }
        else _shield.SetActive(false);
    }

    public void PUTripleShot()
    {
        StartCoroutine(TimerPU(0));
    }

    public void PUSpeed()
    {
        StartCoroutine(TimerPU(1));
    }

    public void PUShield()
    {
        _shield.SetActive(true);
    }

    IEnumerator TimerPU(int x)
    {
        if (x == 0) _tripled = true;
        else if (x == 1) _PUatkSpeedBoost = 2;
        yield return new WaitForSeconds(5f);
        if (x == 0) _tripled = false;
        else if (x == 1) _PUatkSpeedBoost = 1;
    }
}
