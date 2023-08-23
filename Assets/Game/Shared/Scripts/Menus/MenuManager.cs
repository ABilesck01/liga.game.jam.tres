using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string mainMenu;

    private void Start()
    {
        SceneManager.LoadScene(mainMenu, LoadSceneMode.Additive);
    }
}
