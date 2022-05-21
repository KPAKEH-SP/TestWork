using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private int _hp;
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<Rigidbody> _allRigidbodys;
    private Animator _animator;
    private CapsuleCollider _collider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        foreach (var i in _allRigidbodys)
        {
            i.isKinematic = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;
        _healthBar.value = _hp;

        if (_hp <= 0)
        {
            ActivateRagdoll();
            _player.kills++;
        }
    }

    private void ActivateRagdoll()
    {
        _healthBar.gameObject.SetActive(false);
        _animator.enabled = false;
        _collider.enabled = false;

        foreach (var i in _allRigidbodys)
        {
            i.isKinematic = false;
        }
    }
}
