using UnityEngine;
using Photon.Pun;

public class Shooter : MonoBehaviourPun
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    [SerializeField] private float fireRate = 0.5f; // Time between shots
    private float fireTimer; // Timer to track shooting cooldown

    [SerializeField] private ParticleSystem shootingEffect; // Particle effect for shooting

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            // Decrement the fireTimer every frame
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0) && fireTimer <= 0f)
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        // Play audio and shooting effect
        audioManager.PlaySFX(audioManager.shoot);
        shootingEffect.Play();

        // Instantiate the bullet
        GameObject laser = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position, transform.rotation);
        laser.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.MasterClient);

        // Reset the fireTimer
        fireTimer = fireRate;
    }
}
