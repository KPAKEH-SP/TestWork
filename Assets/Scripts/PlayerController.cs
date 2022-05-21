using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int kills;
    public bool onPoint { get; private set; }

    [SerializeField] private List<WaypointInfo> _waypoints;
    [SerializeField] private float _rotateSpeed;

    private NavMeshAgent _agent;
    private Animator _animator;
    private int _currentWaypoint;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (kills == _waypoints[_currentWaypoint].enemyCount && _currentWaypoint < _waypoints.Count-1)
        {
            kills = 0;
            _currentWaypoint++;
            _agent.SetDestination(new Vector3(_waypoints[_currentWaypoint].waypoint.position.x, _waypoints[_currentWaypoint].waypoint.position.y, _waypoints[_currentWaypoint].waypoint.position.z));
        }

        if (Vector3.Distance(this.transform.position, _waypoints[_currentWaypoint].waypoint.position) > 0.3)
        {
            _animator.SetBool("IsRun", true);
            onPoint = false;
        }

        else
        {
            _animator.SetBool("IsRun", false);

            if (_waypoints[_currentWaypoint].focuspoint)
            {
                Vector3 direction = _waypoints[_currentWaypoint].focuspoint.position - transform.position;
                Quaternion rotaion = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotaion, _rotateSpeed * Time.deltaTime);
            }

            onPoint = true;

            if (_waypoints[_currentWaypoint].enemyCount == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void StartGame(GameObject button)
    {
        button.SetActive(false);
        _agent.SetDestination(new Vector3(_waypoints[0].waypoint.position.x, _waypoints[0].waypoint.position.y, _waypoints[0].waypoint.position.z));
    }
}

[System.Serializable]

class WaypointInfo
{
    public Transform focuspoint;
    public Transform waypoint;
    public int enemyCount;
}
