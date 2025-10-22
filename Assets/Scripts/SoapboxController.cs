using UnityEngine;

public class SoapboxController : MonoBehaviour
{
    [SerializeField] private float _motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float _maxSteeringAngle;

    [SerializeField] private Transform _frontLeftWheel;
    [SerializeField] private Transform _frontRightWheel;
    [SerializeField] private Transform _rearLeftWheel;
    [SerializeField] private Transform _rearRightWheel;

    [SerializeField] private WheelCollider _frontLeftCollider;
    [SerializeField] private WheelCollider _frontRightCollider;
    [SerializeField] private WheelCollider _rearLeftCollider;
    [SerializeField] private WheelCollider _rearRightCollider;

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isBraking;

    private void FixedUpdate()
    {
        GetInput();
        ApplyForces();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _isBraking = Input.GetKey(KeyCode.Space);
    }

    private void ApplyForces()
    {
        _frontLeftCollider.motorTorque = _verticalInput * _motorForce;
        _frontRightCollider.motorTorque = _verticalInput * _motorForce;

        float currentBrakeForce = _isBraking ? _breakForce : 0.0f;
        _frontLeftCollider.brakeTorque = currentBrakeForce;
        _frontRightCollider.brakeTorque = currentBrakeForce;
        _rearLeftCollider.brakeTorque = currentBrakeForce;
        _rearRightCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        _frontLeftCollider.steerAngle = _horizontalInput * _maxSteeringAngle;
        _frontRightCollider.steerAngle = _horizontalInput * _maxSteeringAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheel(_frontLeftCollider, _frontLeftWheel);
        UpdateWheel(_frontRightCollider, _frontRightWheel);
        UpdateWheel(_rearLeftCollider, _rearLeftWheel);
        UpdateWheel(_rearRightCollider, _rearRightWheel);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheel.rotation = rot;

        wheel.transform.Rotate(Vector3.forward, 90.0f, Space.Self);
    }
}
