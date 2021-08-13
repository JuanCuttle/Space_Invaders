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

	public int lives = 3;

	public bool gameIsPaused;

	// Update is called once per frame
	void Update()
	{

		if (this.gameIsPaused)
        {
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Time.timeScale = 1f;
				this.gameIsPaused = false;
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}

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

    private void LaserDestroyed()
	{
		_laserActive = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
			other.gameObject.layer == LayerMask.NameToLayer("Missile"))
		{
			this.lives--;
			this.points -= 20;
			if (this.lives <= 0)
            {
				Time.timeScale = 0f;
				this.gameIsPaused = true;
			}
		}
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Pontos: " + this.points);
		GUI.Label(new Rect(350, 10, 100, 20), "Vidas: " + this.lives);

		if (this.lives <= 0)
        {
			GUI.Label(new Rect(170, 200, 1000, 1000), "GAME OVER");
		}
	}
}
