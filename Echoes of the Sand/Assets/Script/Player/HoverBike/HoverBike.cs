using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class HoverBike : MonoBehaviour
{
    [Header("Controle")]
    [SerializeField] private float speed = 10f;                                                 //vitesse du HoverBike
    [SerializeField] internal float vitesse = 0f;
    [SerializeField] private float boostSpeed = 3000f;                                          //vitesse du Hoverbike en boost
    private bool isBoost = false;
    [SerializeField] private float vitesseRotation = 50f;                                       //vitesse de la rotattion du Hoverbike
    [SerializeField] private float rotationRoll = 15f;                                          //angle desiser lors des virage
    Vector2 input;
    [SerializeField] private float dragOn = 1;
    [SerializeField] private float dragOff = 100;
    private float currentSpeed
    {
        get 
        {
            if (isBoost)
            { return boostSpeed; }
            else
            { return speed; }
        } 
    }

    [Header("Hovering")]
    [SerializeField] private bool hover = false;                                                //bool pour savoir si Hover
    [SerializeField] private float vitesseAjustement = 5f;                                      //vitesse de l'ajustement de l'hover
    [SerializeField] private float distanceHover = 3f;                                          //hauter desirer pour le hover du hoverbike
    [SerializeField] private float distanceHoverOn = 3f;
    [SerializeField] private float distanceHoverOff = 0.5f;
    [SerializeField] internal float maxHover = 10f;                                             //hauter max du hovering
    [SerializeField] internal List<GameObject> listeRayEngine = new List<GameObject>();         //liste des point pour les raycast afin de conaitre hauteur
    private List<float> ListeDistanceSol = new List<float>();                                   //liste de tout les hauteur
    private float distanceAjusteeAuSol;                                                         //moyenne des distance

    [Header("Rotation")]
    [SerializeField] private float distanceRotation = 3f;                                       //distance pour ajuter la rotation avec sel du sol
    [SerializeField] private float distanceRotationOn = 3f;
    [SerializeField] private float distanceRotationOff = 0.3f;

    [SerializeField] private float rotationSpeed = 100f;                                        //vitesse pour attaidre la rotation
    [SerializeField] internal List<GameObject> listeRayRotation = new List<GameObject>();       //liste des point pour les raycast afin de conaitre la rotation
    private Vector3 targetRotation;                                                             //rotation desirer pour le Hoverbike
    private Vector3 resetRotation;                                                              //rotation de base pour le HoverBike
    private float zRotation;                                                                    //angle Z du Hoverbike

    //autre
    [Header("Player Seat")]
    [SerializeField] public bool playerMount = false;
    [SerializeField] internal LayerMask layerMask;
    [SerializeField] GameObject player;
    [SerializeField] CapsuleCollider playerCollider;
    [SerializeField] Transform seat;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] Transform exit;

    [Header("Fuel")]
    [SerializeField] GameObject fuelSlider;
    [SerializeField] float fuelUsedOnBoost = 0.0005f;
    [SerializeField] float minFuelToBoost = 0.3f;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move(input);
        Hover();
        AdjustRotation();

        //cree une nouvelle velocite pour le mouvement
        rb.velocity = new Vector3(rb.velocity.x, AdjustVelocityY(), rb.velocity.z);

        if (playerMount)
        {
            AdjustPlayerPos();
            distanceHover = distanceHoverOn;
            distanceRotation = distanceRotationOn;

          
        }
        else
        {
            distanceHover = distanceHoverOff;
            distanceRotation = distanceRotationOff;
        }

        vitesse = rb.velocity.magnitude;
    }

    private void Move(Vector2 input)
    {
        //get l'axe Horizontal et l'utilise pour tourner l'objet
        float rotation = input.x * vitesseRotation * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, rotation);

        //si tourne ver la broite ajuter l'angle Z
        if (input.x < 0)
        {
            zRotation = rotationRoll;
        }
        //si tourne ver la gauche ajuter l'angle Z
        else if (input.x > 0)
        {
            zRotation = -rotationRoll;
        }
        //si rien ne tourne reset
        else
        { zRotation = 0; }

        //get l'axe vertical pour avencer
        float Vitesse = input.y * currentSpeed;
        rb.AddForce(transform.forward * Vitesse);

        if (isBoost)
        {
            //update le fuel tank
            fuelSlider.GetComponent<Health_Bar>().useFuel(fuelUsedOnBoost);
            if (fuelSlider.GetComponent<Health_Bar>().isEmpty(fuelUsedOnBoost))
            {
                isBoost = false;
            }
        }
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if(context.performed && !fuelSlider.GetComponent<Health_Bar>().isEmpty(minFuelToBoost) )
        {
            isBoost = true;
        }    
        else if(context.canceled)
        {
            isBoost = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Hover()
    {
        //reset la liste des distance
        ListeDistanceSol.Clear();
        //cree et set la diatance total
        float distanceTotal = 0f;

        //passe attraver tout les rayEngine
        for (int i = 0; i < listeRayEngine.Count; i++)
        {
            // Lance un rayon vers le bas depuis la position de l'objet
            Ray ray = new Ray(listeRayEngine[i].transform.position, Vector3.down);
            RaycastHit hit;

            // Vérifie si le rayon touche quelque chose
            if (Physics.Raycast(ray, out hit, maxHover, layerMask))
            {
                // Calcule la distance entre l'objet et le sol
                float distanceActuelleAuSol = Vector3.Distance(listeRayEngine[i].transform.position, hit.point);
                ListeDistanceSol.Add(distanceHover - distanceActuelleAuSol);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                hover = true;
            }
            //sitouche rien
            else
            {
                hover = false;
            }

        }

        //calcule la distance de tout les rayon
        for (int i = 0; i < ListeDistanceSol.Count; i++)
        {
            distanceTotal += ListeDistanceSol[i];
        }

        //calcule la distance a ajuster
        distanceAjusteeAuSol = distanceTotal / listeRayEngine.Count;
    }

    private void AdjustRotation()
    {
        //set les variable
        bool ajusteRotation = false;                //bool pour ajuster la rotation
        Vector3 averageNormal = Vector3.zero;       //vector3 pour la moyenne des normal du sol (l'angle du sol)
        int hitCount = 0;                           //reset le nombre de point qui touche le sol

        //passe atraver tout les point pour faire un ray
        foreach (GameObject engine in listeRayRotation)
        {
            //cree un ray appartir du point et le dirige ver le bas
            Ray ray = new Ray(engine.transform.position, Vector3.down);
            RaycastHit hit;

            //si le rayon touche le sol
            if (Physics.Raycast(ray, out hit, distanceRotation, layerMask))
            {
                //ajoute le normal pour la moyenne
                averageNormal += hit.normal;
                Debug.DrawLine(ray.origin, hit.point, Color.blue);
                ajusteRotation = true;
                hitCount++;
            }
        }

        //if need to ajuste the rotation
        if (ajusteRotation)
        {
            //obtien la moyenne des normal
            averageNormal /= hitCount;

            // Normalize the average normal to remove scaling issues
            averageNormal.Normalize();

            //convertie averageNormal ver un Quaternion
            Quaternion targetQuaternion = Quaternion.FromToRotation(Vector3.up, averageNormal);
            targetRotation = targetQuaternion.eulerAngles;

            //set les valeur
            averageNormal.x *= 90;                          //ajute l'emplitude du pitch
            averageNormal.y = transform.eulerAngles.y;      //prend l'angle de l'objet
            averageNormal.z = zRotation;

            //permet d'ajuster le pitch selon la direction de l'objet
            if (transform.eulerAngles.y < 0 || transform.eulerAngles.y > 180)
            {
                averageNormal.x = -averageNormal.x;

            }

            //set l'agle desiser pour la moyenne des normal
            targetRotation = averageNormal;

        }
        //moins de un rayon touche le sol
        if (hitCount <= 1)
        {
            //set les valeur du reset
            resetRotation.x = 0f;
            resetRotation.y = transform.rotation.eulerAngles.y;
            resetRotation.z = zRotation;

            targetRotation = resetRotation;
            //Debug.Log($"Reset Rotation : {resetRotation}");
            ajusteRotation = false;
        }


        // Obtenez la rotation actuelle de l'objet
        Quaternion currentRotation = transform.rotation;

        // Obtenez la rotation vers laquelle vous souhaitez tourner l'objet
        Quaternion desiredRotation = Quaternion.Euler(targetRotation);

        // Utilisez RotateTowards pour effectuer la rotation
        transform.rotation = Quaternion.RotateTowards(currentRotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnPlayerSeat()
    {
        if (!playerMount)
        {
            playerMount = true;
            playerRb.isKinematic = true;
            player.transform.position = seat.position;
            playerCollider.enabled = false;
            player.transform.parent = this.transform;

            rb.drag = dragOn;
            rb.angularDrag = dragOn;

        }
        else
        {
            playerMount = false;
            player.transform.position = exit.position;
            playerRb.isKinematic = false;
            playerCollider.enabled = true;
            player.transform.parent =null;

            rb.drag = dragOff;
            rb.angularDrag = dragOff;

            input = Vector2.zero;
        }
    }

    private void AdjustPlayerPos()
    {
        player.transform.position = seat.position;
        player.transform.rotation = seat.rotation;
        playerRb.isKinematic = true;

    }

    float AdjustVelocityY()
    {
        if (hover)
        {
            //si hover prend la hauter desirer et l'ajuste
            return distanceAjusteeAuSol * vitesseAjustement;
        }
        else
        {
            //si en chute libre prendre la velociter de base de l'objet
            return rb.velocity.y;
        }

    }
}

