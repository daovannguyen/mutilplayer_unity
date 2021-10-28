using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class Player : Photon.PunBehaviour
{
    public Text healthText;

    public float speed = 20f;
    private float Health = 100;
    private float minHealth = 0;
    private float maxHealth = 100;

    public Text userText;
    public string username;

    public Vector3 camOffset;

    public Rigidbody rb;
    private float x;
    private float z;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(photonView.isMine)
        {
            userText.text = username;
        }   
        else
        {

        }    
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hurt")
        {
           if (photonView.isMine)
            {
                photonView.RPC("Damage", PhotonTargets.All);
            }
        }

    }
    [PunRPC]
    void Damage()
    {
        Health -= 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
            Vector3 camPos = Camera.main.transform.position;
            Vector3 pPos = transform.position;
            Camera.main.transform.position = Vector3.Lerp(camPos, new Vector3(pPos.x, pPos.y, pPos.z) + camOffset, 2f * Time.deltaTime);
        }
        else
        {

        }

        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        else if (Health < minHealth)
        {
            Health = minHealth;
        }

    }

    private void FixedUpdate()
    {
        healthText.text = Health.ToString();
        if (photonView.isMine)
        {
            rb.AddForce(new Vector3(x, 0, z) * speed);
        }
    }

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Health);
            stream.SendNext(username);
        }
        else if (stream.isReading)
        {
            Health = (float)stream.ReceiveNext();
            username = (string)stream.ReceiveNext();
            userText.text = username;
        }
    }
}
