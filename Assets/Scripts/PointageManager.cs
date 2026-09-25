using TMPro;
using UnityEngine;


public class PointageManager : MonoBehaviour
{
    
    [SerializeField] TMP_Text texteCoup;
    [SerializeField] Balle balle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AfficherPoints();
    }

    void AfficherPoints()
    {
        texteCoup.text = $"Coups: {balle.coupFait}";// initialiser le score
    }

    void Update()
    {
        AfficherPoints();
    }

}
