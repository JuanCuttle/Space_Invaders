using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{

    public float appearingRate = 30.0f;

    public float speed = 2.0f;

    private bool isAppearing;

    private Vector3 startingPosition = new Vector3(-20.0f, 13.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = this.startingPosition;
        this.isAppearing = true;
        InvokeRepeating(nameof(MakeAppearance), this.appearingRate, this.appearingRate);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (this.transform.position.x >= (rightEdge.x + 1.0f))
        {
            this.transform.position = this.startingPosition;
            this.isAppearing = false;
        } else if (this.isAppearing)
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
    }

    private void MakeAppearance()
    {
        this.transform.position = this.startingPosition;
        this.isAppearing = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.gameObject.SetActive(false);

            Player player = FindObjectOfType<Player>();
            player.points += 50;
        }
    }
}
