using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterStats stats;
    Animator animator;
    [SerializeField]
    float maxSpeed = 1f;
    [SerializeField]
    float rotationSpeed = 4f;

    bool canWalk;

    // Use this for initialization
    void Start () {
        stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        canWalk = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (canWalk)
        {
            float input = Input.GetAxis("Horizontal");

            Walk(input);
        }
    }

    void Walk(float input)
    {
        if (input >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Clamp(input * rotationSpeed, -1, 1), 1, 1);
        }
        transform.Translate(Vector3.right * input * Time.deltaTime * maxSpeed);

        if (input == 0)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Combat Zone"))
        {
            canWalk = false;
            animator.SetBool("IsWalking", false);
        }
    }
}
