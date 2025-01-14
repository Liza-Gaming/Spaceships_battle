using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerHealth : MonoBehaviourPunCallbacks
{
    public int maxHealth = 5;
    public int currentHealth;
    public Image healthBar;
    public Gradient gradient;

    private PhotonView photonView;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    [PunRPC]
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }


    public void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBar.fillAmount = healthPercentage;
        healthBar.color = gradient.Evaluate(healthPercentage);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Laser"))
        {
            audioManager.PlaySFX(audioManager.hit);
            Destroy(other.gameObject);
            TakeDamage(1);
        }
        if (other.gameObject.CompareTag("Meteor"))
        {
            audioManager.PlaySFX(audioManager.hit);
            Destroy(other.gameObject);
            TakeDamage(1);
        }

    }
}
