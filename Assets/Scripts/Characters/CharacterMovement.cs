using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    private enum targetType { NaN, Ground, Bush }
    private targetType _target = targetType.NaN;

    private bool _selectedByPlayer = false, _running = false, _recentSelected = false;
    private static readonly int _GROUND_LAYER = 1 << 6, _BUSH_LAYER = 1 << 7;
    private float _actualWalkingSpeed, _actualGatheringSpeed;
    private string _idleTrigger = "Idle", _runningTrigger = "Running", _gatheringTrigger = "Gathering";
    private Animator _animator;
    private Camera _mainCamera, _uiCamera;
    private NavMeshAgent _agent;
    private Ray _ray, _uiRay;
    private RaycastHit _hit, _uiHit;
    private CharacterProperties _properties;
    private GameObject _selectedGameObject;
    private TimeSystem _timeSystemScript;

    void Start()
    {
        _timeSystemScript = FindObjectOfType<TimeSystem>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _uiCamera = Camera.allCameras.Where(x => x.name == "UICamera").First();
        _properties = GetComponent<CharacterProperties>();

        SetWalkingSpeed();
        SetGatheringSpeed();
    }

    void LateUpdate()
    {
        if (CheckWalkingSpeed()) { SetWalkingSpeed(); }
        if (CheckGatheringSpeed()) { SetGatheringSpeed(); }

        SetCharacterTarget();

        if (_timeSystemScript.GetGameSpeed() == 0) { _agent.velocity = Vector3.zero; }

        SetCharacterAnimation();

        if (_recentSelected) { _recentSelected = false; }
    }

    bool CheckWalkingSpeed()
    {
        if (_actualWalkingSpeed != _properties.walkingSpeed * _timeSystemScript.GetGameSpeed())
        {
            return true;
        }
        return false;
    }

    void SetWalkingSpeed()
    {
        _actualWalkingSpeed = _properties.walkingSpeed * _timeSystemScript.GetGameSpeed();
        _agent.speed = _actualWalkingSpeed;
        _animator.SetFloat("WalkingAnimationSpeed", _actualWalkingSpeed / 10f);
    }
    bool CheckGatheringSpeed()
    {
        if (_actualGatheringSpeed != _properties.gatheringSpeed * _timeSystemScript.GetGameSpeed())
        {
            return true;
        }
        return false;
    }

    void SetGatheringSpeed()
    {
        _actualGatheringSpeed = _properties.gatheringSpeed * _timeSystemScript.GetGameSpeed();
        _animator.SetFloat("GatheringAnimationSpeed", _actualGatheringSpeed / 10f);
    }

    void GatheringAnimationEnd()
    {
        int consumptionValue = _selectedGameObject.GetComponent<BushScript>().GetOneUnit();

        _properties.foodLevel += consumptionValue;

        if (_properties.foodLevel > 100) { _properties.foodLevel = 100; }

        if (consumptionValue == 0 || _properties.foodLevel >= 95)
        {
            _properties.NowDo = CharacterProperties.DoList.Idle;
            _animator.SetTrigger(_idleTrigger);
        }
    }

    void SetCharacterTarget()
    {
        if (Input.GetMouseButtonDown(0) && _selectedByPlayer && !_recentSelected)
        {
            if (PointerIsOverUI(Input.mousePosition)) { return; }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, _BUSH_LAYER))
            {
                Transform bushTransform = _hit.transform.GetComponentsInChildren<Transform>().Where(x => x.name == "CollectionPoint").First();
                _agent.destination = bushTransform.position;
                _target = targetType.Bush;
                _selectedGameObject = _hit.transform.gameObject;

                if (Vector3.Distance(transform.position, bushTransform.position) <= 1f)
                {
                    _properties.NowDo = CharacterProperties.DoList.Gathering;
                    _animator.SetTrigger(_gatheringTrigger);
                }
            }
            else if (Physics.Raycast(_ray, out _hit, 1000f, _GROUND_LAYER))
            {
                _agent.destination = _hit.point;
                _target = targetType.Ground;
            }
        }
    }

    bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };

    GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    void SetCharacterAnimation()
    {
        if (_agent.velocity.sqrMagnitude > 0.15f)
        {
            if (!_running)
            {
                _running = true;
                _animator.SetTrigger(_runningTrigger);
                _properties.NowDo = CharacterProperties.DoList.Walking;
            }
        }
        else if (_running)
        {
            _running = false;

            if (_target == targetType.Ground)
            {
                _properties.NowDo = CharacterProperties.DoList.Idle;
                _animator.SetTrigger(_idleTrigger);
            }
            else if (_target == targetType.Bush)
            {
                _properties.NowDo = CharacterProperties.DoList.Gathering;
                _animator.SetTrigger(_gatheringTrigger);
            }
        }
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
