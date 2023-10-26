using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionJeu : MonoBehaviour
{
    [SerializeField] private GameObject _enemy = default;
    [SerializeField] private GameObject _enemiesBox = default;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-9, 9), 6, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemiesBox.transform;
        }
    }
}
