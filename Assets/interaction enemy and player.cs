using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player; // assign in Inspector
    [SerializeField] private Enemy[] enemies; // Supports multiple enemies!


    void Start()
    {


        Debug.Log("Battle Start!");


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Player attacks Enemy!");
            // Loop through all enemies in the array
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.TakeHit(5); // Example: deal 5 damage to each enemy at start
                }
            }
        }
    }
}
