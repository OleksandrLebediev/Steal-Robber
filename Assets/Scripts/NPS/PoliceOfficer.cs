using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceOfficer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();       
    }

    private void Start()
    {
        StartCoroutine(State());
    }

    private IEnumerator State()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            _animator.SetTrigger("waving");
            yield return null;  
        }
    }
}
