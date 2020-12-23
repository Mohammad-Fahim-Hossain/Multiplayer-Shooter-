using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    private int maxHealth;

    public Color minColor = Color.red;
    public Color maxColor = Color.green;

    private SpriteRenderer rend;

    public float InitalLength = 0.2f;
    public float CurrentLegth = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Player.GetComponent<PlayerHealth>().startingHealth;
        rend = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float fraction = (float)Player.GetComponent<PlayerHealth>().currentHealth / maxHealth;
        rend.color = Color.Lerp(minColor, maxColor, Mathf.Lerp(0, 1, Player.GetComponent<PlayerHealth>().currentHealth / maxHealth));

        transform.localScale = new Vector3(InitalLength * fraction, transform.localScale.y, transform.localScale.z);


        transform.LookAt(Camera.main.transform);
    }
}
