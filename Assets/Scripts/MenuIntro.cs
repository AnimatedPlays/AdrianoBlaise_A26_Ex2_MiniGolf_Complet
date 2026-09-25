using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuIntro : MonoBehaviour
{
    [SerializeField] TMP_Text textepoints;

    void Start()
    {
        int coupFait = PlayerPrefs.GetInt("coupFait", 0);
        textepoints.text = $"Dernier score: {coupFait} coups";
    }
    public void Demarrer()
    {
        SceneManager.LoadScene("Jeu");
    }
}
