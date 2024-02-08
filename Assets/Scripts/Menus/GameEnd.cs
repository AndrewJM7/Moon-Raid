using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // Variables for aliens being dead and objects being beamed
    private static bool allBeamed = false;
    private static bool allDead = false;

    public static void AllBeamed()
    {
        allBeamed = true;
        CheckEndGame();
    }

    public static void AllDead()
    {
        allDead = true;
        CheckEndGame();
    }

    private static void CheckEndGame()
    {
        if (allDead && allBeamed)
        {
            // Creates a delay before the game ends
            Instance.StartCoroutine(EndGameWithDelay(2f));
        }
    }

    private static GameEnd instance;

    private static GameEnd Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameEnd").AddComponent<GameEnd>();
            }
            return instance;
        }
    }

    private static IEnumerator EndGameWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Load game end scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

        // Allows cursor movement in menu
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}

