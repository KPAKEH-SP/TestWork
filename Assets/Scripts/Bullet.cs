using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _movepoint = new Vector3(0, 0, 0);

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movepoint, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(1);
        }

        this.gameObject.SetActive(false);
    }

    public void SetMovepoint(Vector3 movepoint)
    {
        _movepoint = movepoint;
    }
}
