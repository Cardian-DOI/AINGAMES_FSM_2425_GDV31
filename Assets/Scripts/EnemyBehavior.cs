using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float maxForwardSpeed = 5.0f;

    private Transform turret;
    private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;
    private float turretRotSpeed = 10.0f;
    private Vector3 PlayerPos;

    [SerializeField]
    private GameObject Player;

    //Bullet shooting rate
    public float shootRate = 2.0f;
    private float elapsedTime;

    public Transform EnemyTankTarget;


    private void Start()
    {
        //Tank Settings
        rotSpeed = 150.0f;
        Player = GameObject.Find("Player");
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
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position + new Vector3(0, 0, 0));

        PlayerPos = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        
        Vector3 direction = (EnemyTankTarget.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turretRotSpeed * 0.4f);
        transform.position = Vector3.MoveTowards(transform.position, PlayerPos, maxForwardSpeed * Time.deltaTime);

    }

    private void UpdateWeapon()
    {
        if (EnemyTankTarget != null)
        {
            Vector3 direction = (EnemyTankTarget.position - transform.position).normalized;
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

