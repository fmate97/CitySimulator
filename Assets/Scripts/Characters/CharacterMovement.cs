using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private bool _selectedByPlayer = false, _running = false, _recentSelected = false;
    private static readonly int _GROUND_LAYER = 1 << 6;
    private float _actualWalkingSpeed;
    private string _runningBool = "Running";
    private Animator _animator;
    private Camera _mainCamera;
    private NavMeshAgent _agent;
    private Ray _ray;
    private RaycastHit _hit;
    private CharacterProperties _properties;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _properties = GetComponent<CharacterProperties>();

        _actualWalkingSpeed = _properties.walkingSpeed;
        _agent.speed = _actualWalkingSpeed;
        _animator.SetFloat("WalkingAnimationSpeed", _actualWalkingSpeed / 10f);
    }

    void LateUpdate()
    {
        if (_actualWalkingSpeed != _properties.walkingSpeed)
        {
            _actualWalkingSpeed = _properties.walkingSpeed;
            _agent.speed = _actualWalkingSpeed;
            _animator.SetFloat("WalkingAnimationSpeed", _actualWalkingSpeed / 10f);
        }

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
                _properties.NowDo = CharacterProperties.DoList.Walking;
            }
        }
        else if (_running)
        {
            _running = false;
            _animator.SetBool(_runningBool, false);
            _properties.NowDo = CharacterProperties.DoList.Idle;
        }

        if (_recentSelected) { _recentSelected = false; }
    }

    public void SelectedCharacter()
    {
        _selectedByPlayer = true;
        _recentSelected = true;
    }

    public void UnselectedCharacter()
    {
        _selectedByPlayer = false;
    }
}
