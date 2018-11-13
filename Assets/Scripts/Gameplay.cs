using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour {

    public GameObject Bullet;
    public GameObject Platform;
    private Transform trans;
    private float cooldown = 0.05f;
    private float sum = 0.0f;
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetButton("Fire1"))
        {
            sum += Time.deltaTime;
            if (sum>cooldown)
            {
                sum = 0.0f;
            
                GameObject temp_bullet;
                temp_bullet = Instantiate(Bullet, transform.position + transform.forward*3f, transform.rotation);

                Rigidbody temp_rigid;
                temp_rigid = temp_bullet.GetComponent<Rigidbody>();
                temp_rigid.AddForce(transform.forward * 1000.0f);
                print(temp_bullet.transform.position);
                print(transform.forward);
                Destroy(temp_bullet, 120.0f);
            }
        }   
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(Platform, transform.position + transform.forward*2f, Quaternion.identity);
        }
    }
}
