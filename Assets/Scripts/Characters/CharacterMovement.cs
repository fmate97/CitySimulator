using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    private bool _selectedByPlayer = false, _recentSelected = false, _running = false;
    private static readonly int _GROUND_LAYER = 1 << 6;
    private string _charactersTag = "Characters", _runningBool = "Running";
    private Animator _animator;
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private Ray _ray;
    private RaycastHit _hit;
    private List<GameObject> _otherCharacters;

    void Start()
    {
        _otherCharacters = new List<GameObject>(GameObject.FindGameObjectsWithTag(_charactersTag));
        _otherCharacters.Remove(gameObject);

        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _selectedByPlayer && !_recentSelected)
        {
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, 1000f, _GROUND_LAYER))
            {
                _agent.destination = _hit.point;
            }
        }

        if (_agent.velocity.sqrMagnitude > 0.15f)
        {
            if (!_running)
            {
                _running = true;
                _animator.SetBool(_runningBool, true);
            }
        }
        else if (_running)
        {
            _running = false;
            _animator.SetBool(_runningBool, false);
        }
    }

    void LateUpdate()
    {
        if (_recentSelected) { _recentSelected = false; }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !_selectedByPlayer)
        {
            _selectedByPlayer = true;
            _recentSelected = true;

            foreach (GameObject character in _otherCharacters)
            {
                character.GetComponent<CharacterMovement>().SelectedAnotherCharacter();
            }
        }
    }

    public void SelectedAnotherCharacter()
    {
        _selectedByPlayer = false;
    }
}
