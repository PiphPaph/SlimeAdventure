using System;
using System.Collections;
using UnityEngine;

public class DisableWaterLineCollider : MonoBehaviour
{
    private ChangeSlimeForm _slimeForm;
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _slimeForm = GameObject.Find("Player").GetComponent<ChangeSlimeForm>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_slimeForm == null) return; // Защита от null
        
        if (_slimeForm.IsIronSlime)
        {
            _collider.enabled = false;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_slimeForm == null) return; // Защита от null
        
        if (_slimeForm.IsIronSlime)
        {
            _collider.enabled = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_slimeForm == null) return; // Защита от null
        
        if (_slimeForm.IsIronSlime)
        {
            _collider.enabled = false;
            StartCoroutine(EnableWaterLine());
        }
    }

    private IEnumerator EnableWaterLine()
    {
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
    }
}
