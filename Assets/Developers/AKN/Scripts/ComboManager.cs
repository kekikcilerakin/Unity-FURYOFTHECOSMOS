using System.Collections;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public Animator animator;

    [Header("Combo Settings")]
    public int comboStep = 0;
    public float comboResetTime = 2.0f; // Time allowed between combo attacks
    public float attackCooldown = 0.25f; // Delay between each attack
    public float comboCooldown = 2.0f; // Cooldown after completing a combo

    private float lastAttackTime;
    public bool isAttacking = false;
    public bool isComboOnCooldown = false;

    private void Update()
    {
        if (isComboOnCooldown) return;


        if (comboStep != 0 && Time.time - lastAttackTime > comboResetTime)
        {
            ResetCombo();
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            PerformCombo();
        }
    }

    private void PerformCombo()
    {
        lastAttackTime = Time.time;
        isAttacking = true;
        comboStep = (comboStep % 3) + 1;

        animator.SetTrigger("Attack" + comboStep);

        if (comboStep == 3)
        {
            StartCoroutine(StartComboCooldown());
        }
        else
        {
            StartCoroutine(ResetAttack());
        }
    }

    private void ResetCombo()
    {
        comboStep = 0;
        isAttacking = false;
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private IEnumerator StartComboCooldown()
    {
        isComboOnCooldown = true;
        yield return new WaitForSeconds(comboCooldown);
        isComboOnCooldown = false;
        ResetCombo();
    }
}
