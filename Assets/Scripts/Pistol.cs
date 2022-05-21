using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnpoint;
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _shootDelay;
    [Header("Настройка пула")]
    [SerializeField] private int _poolCount = 15;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Bullet _prefab;

    private ClassPool<Bullet> _pool;
    private float _timer;

    private void Start()
    {
        _pool = new ClassPool<Bullet>(_prefab, _poolCount, this.transform);
        _pool.autoExpand = _autoExpand;
    }


    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (Input.touchCount == 1 && _player.onPoint && _timer <= 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                CreateBullet(hit.point);
            }

            _timer = _shootDelay;
        }

    }

    private void CreateBullet(Vector3 movepoint)
    {
        var bullet = _pool.GetFreeElement();
        bullet.transform.position = _bulletSpawnpoint.position;
        bullet.SetMovepoint(movepoint);
    }
}