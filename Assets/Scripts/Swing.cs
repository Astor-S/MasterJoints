using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private float _swingSpeed = 20f;
    [SerializeField] private float _swingForce = 200f;
    [SerializeField] private float _targetAngle = 45f;

    private HingeJoint _hingeJoint;
    private float _currentAngle = 0f; 
    private bool _isSwinging = false;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isSwinging == false)
        {
            _isSwinging = true;
            _currentAngle = 0f;
        }

        if (_isSwinging)
        {
            float targetVelocity = Mathf.Sign(_targetAngle - _currentAngle) * _swingSpeed;

            _hingeJoint.motor = new JointMotor()
            {
                targetVelocity = targetVelocity,
                force = _swingForce,
                freeSpin = false
            };

            _currentAngle += targetVelocity * Time.deltaTime;

            if (Mathf.Abs(_currentAngle) >= _targetAngle)
            {
                _isSwinging = false;

                _hingeJoint.motor = new JointMotor()
                {
                    targetVelocity = 0,
                    force = 0,
                    freeSpin = false
                };
            }
        }
    }
}