using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField] private Spoon _spoon;
    [SerializeField] private Core _core;
    [SerializeField] private SpringJoint _springJoint;

    [SerializeField] private float _catapultRotationAngle = 45f;
    [SerializeField] private float _reloadDelay = 1f;

    private bool isLaunched = false;
    private Rigidbody _coreRigidbody;
    private Vector3 _initialSpoonRotation;

    private void Awake()
    {
        _coreRigidbody = _core.GetComponent<Rigidbody>();
        _initialSpoonRotation = _spoon.transform.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isLaunched == false)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && isLaunched)
        {
            ReloadCatapult();
        }
    }

    private void Shoot()
    {
        Quaternion targetRotation = Quaternion.Euler(_catapultRotationAngle, 0, 0); // Создаем вращение по оси X
        _spoon.transform.rotation = targetRotation * _spoon.transform.rotation; // Комбинируем с текущим вращением

        _core.transform.parent = null;
        isLaunched = true;

        _coreRigidbody.AddForce(Vector3.forward * 5, ForceMode.Impulse);

    }

    private void ReloadCatapult()
    {
        StartCoroutine(ResetCoroutine());
        isLaunched = false;
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(_reloadDelay);

        _core.transform.parent = _spoon.transform;
        _core.transform.localPosition = Vector3.zero;
        _coreRigidbody.velocity = Vector3.zero;
        _coreRigidbody.angularVelocity = Vector3.zero;

        _spoon.transform.localEulerAngles = _initialSpoonRotation;
    }
}