using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [Header("Attack Controls")]
    [SerializeField] private string attackButton = "Fire1";

    [Header("Attack Properties")]
    [SerializeField] private float damage = 1;
    [SerializeField] private float range = 3;
    [SerializeField] private float coolDown = 0.5f;
    [SerializeField] private float attackSize = 0.5f;
    [SerializeField] private LayerMask attackLayer;   

    [Header("Debug Values")]
    [SerializeField] private bool hit;
    [SerializeField] private float nextFire;

    [Header("Trace Debug")]
    [SerializeField] private bool showTrace = true;
    [SerializeField] private float traceDuration = 1;
    [SerializeField] private Color traceColour = Color.red;

    public void CheckAttack(int direction)
    {
        if (Input.GetButton(attackButton) && Time.time > nextFire)
        {
            Attack(direction);
        }
    }

    private void Attack(int direction)
    {
        nextFire = Time.time + coolDown;

        RaycastHit2D objectCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right * direction, range, attackLayer);
        RaycastHit2D objectCheckBottom = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - attackSize), Vector2.right * direction, range, attackLayer);
        RaycastHit2D objectCheckTop = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + attackSize), Vector2.right * direction, range, attackLayer);

        if (showTrace)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), Vector3.right * direction * range, traceColour, traceDuration);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - attackSize, transform.position.z), Vector3.right * direction * range, traceColour, traceDuration);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + attackSize, transform.position.z), Vector3.right * direction * range, traceColour, traceDuration);
        }

        if (objectCheck || objectCheckBottom || objectCheckTop)
        {
            //ATTACK ENEMY
        }
    }
}
