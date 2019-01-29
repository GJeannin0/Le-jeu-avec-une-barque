using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCondition : MonoBehaviour { 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthBar.health -= 35f;
            if (HealthBar.health <= 0f)
            {
                SceneManager.LoadScene("Lose");
            }

        }
        
    }
}
