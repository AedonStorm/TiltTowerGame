using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonAplastador : MonoBehaviour {

    public Transform _finalPositionTransform;
    public Collider2D _ColliderDetector;

    private Vector3 _startPosition;
    private Vector3 _finalPosition;
    private Vector3 _currentVelocity;

    private enum State {Smoth, Smash, Return}
    private State _currentState;

    void Start () {
        _startPosition = transform.position;
        _finalPosition = _finalPositionTransform.position;
        _currentState = State.Smoth;
    }

    void Update()
    {
        switch (_currentState)
        {
            case State.Smoth:
                transform.position = Vector3.SmoothDamp(transform.position, _startPosition - Vector3.up, ref _currentVelocity, 1f);
                if(Vector3.Distance(transform.position, _startPosition - Vector3.up) <= 0.1f)
                {
                    _currentState = State.Smash;
                }
                break;
            case State.Smash:
                _ColliderDetector.enabled = true;
                transform.position = Vector3.SmoothDamp(transform.position, _finalPosition - Vector3.up, ref _currentVelocity, 0.2f);
                if (Vector3.Distance(transform.position, _finalPosition) <= 0.1f)
                {
                    Camera.main.GetComponent<CameraShake>().Ini(0.2f,0.2f);
                    _currentState = State.Return;
                }
                break;
            case State.Return:
                _ColliderDetector.enabled = false;
                transform.position = Vector3.SmoothDamp(transform.position, _startPosition, ref _currentVelocity, 1f);
                if (Vector3.Distance(transform.position, _startPosition) <= 0.1f)
                {
                    _currentState = State.Smoth;
                }
                break;
            default:
                break;
        }
    }
}
