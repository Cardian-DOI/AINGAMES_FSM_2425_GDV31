using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float maxForwardSpeed = 10.0f;
    [SerializeField]
    private float maxBackwardSpeed = -10.0f;

    private Transform turret;
    private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;
    private float turretRotSpeed = 10.0f;

    //Bullet shooting rate
    public float shootRate = 2.0f;
    private float elapsedTime;

    public Transform PlayerTank;

    private void Start()
    {
        //Tank Settings
        rotSpeed = 150.0f;

        //Get the turret of the tank
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;
    }

    private void OnEndGame()
    {
        // Don't allow any more control changes when the game ends
        this.enabled = false;
    }

    private void Update()
    {
        UpdateControl();
        UpdateWeapon();
    }

    private void UpdateControl()
    {//AIMING WITH THE MOUSE
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0, 0));

        // Generate a ray from the cursor position
        Ray RayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Determine current speed
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, 7.0f * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    private void UpdateWeapon()
    {
        if (PlayerTank != null)
        {
            Vector3 direction = (PlayerTank.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, Time.deltaTime * turretRotSpeed);

            if (Time.time >= elapsedTime)
            {
                elapsedTime = Time.time + shootRate;
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                
            }   
        }
       }
    }

