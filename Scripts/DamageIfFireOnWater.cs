using System.Collections;
using UnityEngine;

public class DamageIfFireOnWater : MonoBehaviour
{
    private ChangeSlimeForm _slimeForm;
    private HealthBar _healthBar;
    private Coroutine _damageCoroutine;
    public float damage = 0.01f;

    private void Start()
    {
        _slimeForm = GameObject.Find("Player").GetComponent<ChangeSlimeForm>();
        _healthBar = GameObject.Find("Player").GetComponent<HealthBar>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_slimeForm == null || _healthBar == null) return;

        if (_slimeForm.IsFireSlime && _damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            _healthBar.health -= damage;
            Debug.Log("🔥 Урон от воды: " + _healthBar.health);
            yield return new WaitForSeconds(0.5f);
        }
    }
}