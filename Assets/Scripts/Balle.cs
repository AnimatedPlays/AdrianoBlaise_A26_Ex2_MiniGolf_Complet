using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using System.Numerics;

public class Balle : MonoBehaviour
{

    [Header("État de jeu")]
    public int coupFait;


    [Header("Paramètres de tir")]
    [SerializeField] float gaugeForce = 0f;
    [SerializeField] Rigidbody rigidbody;


    [Header("Gauge de force")]
    
    [SerializeField] Slider jaugeUI;
    [SerializeField] float forceMin = 0f;
    [SerializeField] float forceMax = 100f;
    [SerializeField] float incremementForce = 0.1f;
    // [SerializeField] GameObject jaugeForceGO;

    // [Header("Input Actions")]
    [SerializeField] InputAction ballCharger;


    // [Header("Composant")]


    [Header("Son")]
    [SerializeField] AudioSource audioSourceBalle;
    [SerializeField] AudioClip sonTerrain;
    [SerializeField] AudioClip sonFin;


    void Start()
    {
        audioSourceBalle = GetComponent<AudioSource>();
        LineRenderBalle = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        jaugeUI.value = 0;
        coupFait = 0;
        peutBouger = true;

        if (PlayerPrefs.HasKey("positionBalle"))
        {
            // on recupere l'information
            string positionJSON = PlayerPrefs.GetString("positionBalle");
            //On convertit dans le type prévu Ex: Vector3, vector2, color, etc.
            Transform.position = JsonUtility.FromJson<Vector3>(positionJSON);
        }
        
    }

    void Update()
    {
        if (ballCharger.WasPressedThisFrame())
        {
            gaugeForce = 0f;
            jaugeUI.value = gaugeForce;
            
        }

        if (ballCharger.IsPressed())
        {
            gaugeForce += incremementForce;
            gaugeForce = Mathf.Clamp(gaugeForce, forceMin, forceMax);
            jaugeUI.value = gaugeForce;
        }

        if (ballCharger.WasReleasedThisFrame())
        {
            // quand la touche est relaché
            gaugeForce = 0f;
            jaugeUI.value = gaugeForce;
            rigidbody.AddForce(direction * gaugeForce * Time.deltaTime, ForceMode.Impulse);
            coupFait++;

            // Permet d'enregistrer la position de la balle
            string positionJSON = JsonUtility.ToJson(Transform.position);
            PlayerPrefs.SetString("positionBalle", positionJSON);
            

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            audioSourceBalle.PlayOneShot(sonTerrain);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "trou")
        {
            rigidbody.linearVelocity = Vector3.Zero;
            
            audioSourceBalle.PlayOneShot(sonFin);
            PlayerPrefs.SetInt("coupFait", coupFait);
            PlayerPrefs.DeleteKey("positionBalle");
            SceneManager.LoadScene("Intro");

            Debug.log("fin du jeu");
        }
    }

    // ===================
    void FrapperBalle()
    {

    }

    void MettreAJourUI()
    {
    }

    // IEnumerator FinJeu()
    // {

    // }

    void SauvegarderScore()
    {

    }

    //=================================
    // Gestion des inputs actions
    void OnEnable()
    {
        ballCharger.Enable();
    }

    void OnDisable()
    {
        ballCharger.Disable();
    }
}
