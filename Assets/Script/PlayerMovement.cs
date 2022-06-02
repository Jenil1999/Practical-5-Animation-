using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] Animator anim;
	[SerializeField] AudioClip DieSound;
	[Header("Running")]
	float horizontal;
    [SerializeField] float MovementSpeed = 5f;
	private bool facingRight = true;
	BoxCollider2D MyFeetCollider;
	CapsuleCollider2D MyBodyCollider2D;
	public AudioSource Audio;
	[SerializeField] Vector2 DeathJump = new Vector2(10f, 10f);
	[SerializeField] float RollSpeed = 30f;
	[SerializeField] float ClimbSpeed = 10f;
	[SerializeField] GameObject Bullet;
	[SerializeField] Transform Gun;

	float GravityScaleatStart;
	float vertical;

	bool IsAlive = true;

	[SerializeField] private float jumpForce = 300f;

	Rigidbody2D rb;

	private void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		MyFeetCollider = GetComponent<BoxCollider2D>();
		GravityScaleatStart = rb.gravityScale;
		MyBodyCollider2D = GetComponent<CapsuleCollider2D>();
	}
    private void Update()
    {
			 horizontal = Input.GetAxis("Horizontal");
			 vertical = Input.GetAxis("Vertical");	
			OnRun();
			OnJump();
			OnRoll();
			OnClimb();
			Die();
			OnFire();
	}
    private void OnRun()
    {
		if(IsAlive)
        {
			anim.SetFloat("Running", Mathf.Abs(horizontal));
			rb.velocity = new Vector2(horizontal * MovementSpeed, rb.velocity.y);

			if (horizontal > 0 && !facingRight)
			{
				Flip(horizontal);
			}

			else if (horizontal < 0 && facingRight)
			{
				Flip(horizontal);
			}
		}
		
	}

    private void Flip(float horizontal)
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnRoll()
    {
		if(IsAlive)
        {
			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				if (!MyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
				{ return; }
				anim.SetBool("Roll", true);
				rb.velocity = new Vector2(horizontal * RollSpeed, rb.velocity.y);
			}
			else
			{
				anim.SetBool("Roll", false);
			}
		}
	
	}

    private void OnJump()
    {
		if(IsAlive)
        {
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (!MyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Climb")))
				{ return; }
				anim.SetBool("Jump", true);
				rb.AddForce(new Vector2(0, jumpForce));
			}
			else
			{
				anim.SetBool("Jump", false);
				rb.AddForce(new Vector2(0, 0));
			}
		}
		
	}

    private void OnClimb()
    {
		if(IsAlive)
        {
			if (!MyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb")))
			{
				rb.gravityScale = GravityScaleatStart;
				anim.SetBool("Climbing", false);
				return;
			}
			rb.gravityScale = 0;
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
			{
				Debug.Log("Touching");
				Vector2 ClimbVelocity = new Vector2(rb.velocity.x, vertical * ClimbSpeed);
				rb.velocity = ClimbVelocity;

				bool playerVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
				anim.SetBool("Climbing", playerVerticalSpeed);
			}
			else
			{
				Vector2 ClimbVelocity = new Vector2(rb.velocity.x, vertical * ClimbSpeed);
				rb.velocity = ClimbVelocity;
				anim.SetBool("Climbing", false);
			}
		}
		
    }

	void Die()
	{
		if (MyBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Hezards")))
		{
			IsAlive = false;
			rb.velocity = DeathJump;
			anim.SetTrigger("Die");
			Audio.mute = true;
			AudioSource.PlayClipAtPoint(DieSound, Camera.main.transform.position , 0.1f);
			Invoke("ReloadScene", 1f);
		}
	}

	void OnFire()
    {
        if (IsAlive) 
		{
			if (Input.GetKeyDown(KeyCode.RightControl) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				anim.SetBool("Fire", true);
				Instantiate(Bullet, Gun.position, transform.rotation);
			}
				
			else
            {
				anim.SetBool("Fire", false);
			}
		}
	}
	public void ReloadScene()
	{
		SceneManager.LoadScene(0);
	}

}
