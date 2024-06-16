using System.Collections;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    public Animator animator;

    public int comboStep = 0;
    private float lastAttackTime;
    public bool isAttacking = false;
    public bool comboOnCooldown = false;
    public float comboResetTime = 1.0f; // Time allowed between combo attacks
    public float attackCooldown = 0.25f; // Delay between each attack
    public float comboCooldown = 1.0f; // Cooldown after completing a combo

    void Update()
    {
        if (comboStep != 0 && Time.time - lastAttackTime > comboResetTime && !comboOnCooldown)
        {
            ResetCombo();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && !comboOnCooldown)
        {
            PerformCombo();
        }
    }

    void PerformCombo()
    {
        lastAttackTime = Time.time;
        isAttacking = true;
        comboStep++;

        if (comboStep > 3)
        {
            ResetCombo(); // Loop back to the first attack
        }

        if (comboStep == 3)
        {
            StartComboCooldown(); // Start the combo cooldown after completing a combo
        }

        switch (comboStep)
        {
            case 1:
                animator.SetTrigger("Attack1");
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
        }

        Invoke("ResetAttack", attackCooldown); // Reset isAttacking after cooldown
    }

    void ResetCombo()
    {
        comboStep = 0;
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void StartComboCooldown()
    {
        comboOnCooldown = true;
        Invoke("ResetComboCooldown", comboCooldown); // Reset combo cooldown after the specified duration
    }

    void ResetComboCooldown()
    {
        comboOnCooldown = false;
    }
}
