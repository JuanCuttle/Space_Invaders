using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public Projectile laserPrefab;

	public float speed = 10.0f;

	private bool _laserActive;

	public int points;

	// Update is called once per frame
	void Update()
	{

		//print(this.points);
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			this.transform.position += Vector3.left * this.speed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{

			this.transform.position += Vector3.right * this.speed * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		if (!_laserActive)
		{
			Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
			projectile.destroyed += LaserDestroyed;
			_laserActive = true;
		}
	}

    public static explicit operator Player(GameObject v)
    {
        throw new NotImplementedException();
    }

    private void LaserDestroyed()
	{
		_laserActive = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
			other.gameObject.layer == LayerMask.NameToLayer("Missile"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Pontos: " + this.points);
	}
}
