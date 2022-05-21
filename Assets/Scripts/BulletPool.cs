using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 15;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Bullet _prefab;

    private ClassPool<Bullet> pool;

    private void Start()
    {
        this.pool = new ClassPool<Bullet>(_prefab, _poolCount, this.transform);
        this.pool.autoExpand = this._autoExpand;
    }
}
