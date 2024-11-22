using System;
using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField] private Spoon _spoon;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private Rigidbody _restingAnchor;
    [SerializeField] private Rigidbody _finishAnchor;

    [SerializeField] private KeyCode _shootKey = KeyCode.X;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;

    [SerializeField] private float _reloadDelay = 1f;

    private bool isLaunched = false;

    public event Action OnReloadComplete;

    private void Update()
    {
        if (Input.GetKeyDown(_shootKey) && isLaunched == false)
            Shoot();
        
        if (Input.GetKeyDown(_reloadKey) && isLaunched)
            ReloadCatapult();
    }

    private void Shoot()
    {
        isLaunched = true;

        _springJoint.connectedBody = _finishAnchor;
    }

    private void ReloadCatapult()
    {
        _springJoint.connectedBody = _restingAnchor;
        StartCoroutine(ResetCoroutine());
        isLaunched = false;
    }

    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(_reloadDelay);

        OnReloadComplete?.Invoke();
    }
}