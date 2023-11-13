using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance;

    [SerializeField] private GameObject _enemy = default;
    [SerializeField] private GameObject _enemiesBox = default;

    private bool _stopSpawn  = false;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EndGame()
    {
        _stopSpawn = true;
    }


    IEnumerator Spawn()
    {
        while (!_stopSpawn)
        {
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-9, 9), 6, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemiesBox.transform;
            yield return new WaitForSeconds(3f);
        }
    }
}
