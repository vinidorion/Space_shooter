using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private GameObject _laser = default;
    [SerializeField] private GameObject _beam = default;
    private float _canShoot = -1;

    [Header("Stats")]
    [SerializeField] private float _speed = 12;
    [SerializeField] private int _life = default;
    [SerializeField] private float _atkSpeed = 0.5f;
    private GameObject _atk = default;


    [Header("Limites")]
    [SerializeField] private float _maxX = 11;
    [SerializeField] private float _minX = -11;
    [SerializeField] private float _maxY = 5;
    [SerializeField] private float _minY = -3.5f;



    private void Start()
    {
        transform.position = new Vector3(0, -3.5f, 0);
        _atk = _laser;
    }

    private void Update()
    {
        Mouvement();
        TirerLaser();
        ChangerLaser();

    }

    private void ChangerLaser()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_atk == _laser)
            {
                _atk = _beam;
                _atkSpeed = 0;

            }
            else if (_atk == _beam)
            {
                _atk = _laser;
                _atkSpeed = 0.5f;
            }
        }
    }

    private void TirerLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canShoot)
        {
                Instantiate(_atk, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                _canShoot = Time.time + _atkSpeed;
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

    public void SetLife(int newLife)
    {
        _life = newLife;
    }
}
