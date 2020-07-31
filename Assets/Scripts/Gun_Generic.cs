using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Gun_Generic : MonoBehaviour
{

    // Ben Soars
    [Header("Weapon Ammo")]
    [Space(2)]
    public string m_name = "GUN";
    public int m_maxAmmo = 6; // the maximum ammo
    public int m_currentAmmo = 6; // the current ammo
    public int m_ammoPerShot = 1; // how much ammo shooting will consume

    [Space(2)]
    [Header("Weapon Accuracy")]
    [Space(2)]
    public bool m_isAccurate = true; // if the gun is accurate or not
    public List<float> m_accuracyMinRange = new List<float>(); // min range for accuracy
    public List<float> m_accuracyMaxRange = new List<float>(); // max range for accuracy
    private List<float> m_accuracyGenerated = new List<float>(); // the generated accuracy
    private Vector3 m_newAccuracy;

    [Space(2)]
    [Header("Weapon Aim Sights")]
    [Space(2)]
    public bool m_canAim = false;  // allows the weapon to aim down sights
    private bool m_isAiming = false; // if the weapon is currently being aimed
    public Camera m_playerCam; // the player camera, allows for the changing of FOV for aiming

    //Kurtis Watson and Ben Soars
    [Space(2)]
    [Header("Weapon Other Stats")]
    [Space(2)]
    public float m_shotForce = 100; // how much force the bullet will be shot for, it hitscan leave blank
    public float m_bulletDamage; // damage of the shot
    public int m_minBulletDamage; // the minimum damage for the bullet
    public int m_maxBulletDamage; // the macimum damage for the bullet

    [Space(2)]
    [Header("Shot Type")]
    [Space(2)]
    public Rigidbody m_physicalBullet; // leave this blank if the gun is hitscan
    public Transform m_shotPoint; // the point where the bullet is shot from
    private Rigidbody m_shotBullet;  // the bullet that's physical

    private RaycastHit m_hitscanCast; // the hitscan raycast
    public GameObject hitSpark;

    private Text m_ammoCount; // the ui element displaying the current ammo
    private Player_Controller m_player; // get the player component

    [Space(2)]
    [Header("Other")]
    [Space(2)]
    public Animator m_gunAnim; // the animations for gun

    //Kurtis Watson

    public GameObject m_hitDamageText;

    void Start()
    {
        m_ammoCount = GameObject.Find("AmmoCount").GetComponent<Text>();
        m_player = GameObject.FindObjectOfType<Player_Controller>();
      

        f_updateUI();
    }


    void f_ShootGun()
    {
        if (m_gunAnim) // if the gun has an animator
        {
            m_gunAnim.SetTrigger("Shot"); // play shot animations
        }
            for (int i = 0; i < m_ammoPerShot; i++)
        {
            if (!m_isAccurate)
            {
                m_newAccuracy = f_BulletSpread(); // generate a new accuracy if the gun isn't set to be accurate
            }
            else
            {
                m_newAccuracy = m_shotPoint.forward;  // is accurate, uses default accuracy
            }

            if (m_physicalBullet) // if a physical bullet is there
            {
                m_shotBullet = Instantiate(m_physicalBullet, m_shotPoint.transform.position, Quaternion.identity) as Rigidbody; // shoot bullet
                m_shotBullet.AddForce(m_newAccuracy * m_shotForce); // push bullet out
            }
            else
            {
                if (Physics.Raycast(m_shotPoint.position, m_newAccuracy, out m_hitscanCast, Mathf.Infinity)) // shoot out a raycast for hitscan
                { 
                    Debug.DrawRay(m_shotPoint.position, m_newAccuracy * m_hitscanCast.distance, Color.yellow); // draw line only viewable ineditor
                    Instantiate(hitSpark, m_hitscanCast.point, Quaternion.identity); // create hitspark at hit point

                    if (m_hitscanCast.transform.gameObject.CompareTag("Enemy")) // if an enemy was hit take away their health based on damage
                    {
                        m_hitscanCast.transform.gameObject.GetComponent<Enemy_Controller>().m_enemyHealth -= m_bulletDamage; // damage the player
                        //Kurtis Watson
                        GameObject m_textObject = Instantiate(m_hitDamageText, m_hitscanCast.point, Quaternion.identity); // spawn a damage 
                        m_textObject.GetComponentInChildren<TextMeshPro>().text = "" + m_bulletDamage; // assign damage dealt to the text
                    }
                }
            }

            m_accuracyGenerated.Clear(); // clear the generated accuracy
            m_currentAmmo -= 1; // decrease total ammo counter by the ammo consuption of the gun
        }
    }

    Vector3 f_BulletSpread()
    {
        for (int j = 0; j < m_accuracyMaxRange.Count; j++)
        {
            m_accuracyGenerated.Add(Random.Range(m_accuracyMinRange[j], m_accuracyMaxRange[j])); // randomly generate accuracy
        }
        Vector3 accuracy = new Vector3(m_shotPoint.forward.x + m_accuracyGenerated[0], m_shotPoint.forward.y + m_accuracyGenerated[1], m_shotPoint.forward.z + m_accuracyGenerated[2]); // send back accuracy
        return accuracy; // return the accuracy
    }

    void Update()
    {
        m_bulletDamage = Random.Range(m_minBulletDamage, m_maxBulletDamage);  // generate random damage
        if (m_player.m_isPlayerActive == true && m_player.isUsingLadder == false) // if the player is active and not on a ladder
            {
                if (m_player.m_isSprinting == false) // if the player isn't sprintin
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0)) // and the player has fired their weapon
                    {
                        if (m_currentAmmo > 0) // if there is ammo
                        {
                            f_ShootGun(); // shoot the gun, run the shoot function
                        }
                        f_updateUI(); // update the ui
                    }
                }

            if (m_canAim == true) // if the player is able to aim down sights with this weapon
            {
                if (Input.GetKey(KeyCode.Mouse1) && m_player.m_isSprinting == false) // if they're holding down right mouse and aren't sprinting
                {
                    m_isAiming = true; // they are aiming
                    m_isAccurate = true; // gun becomes accurate
                    m_playerCam.fieldOfView = 40; // change FOV to be smaller
                }
                else
                {
                    m_isAiming = false; // no longer aiming
                    m_isAccurate = false; // gun becomes inaccurate
                    m_playerCam.fieldOfView = 60; // FOV gets reset
                }
            }
            else
            {
                // else reset everything
                m_isAiming = false;
                m_isAccurate = false;
                m_playerCam.fieldOfView = 60;
            }
        }

        if (m_gunAnim) // if there is a gun animatior
        {
            // update gun animations
            m_gunAnim.SetBool("Aim", m_isAiming);
            m_gunAnim.SetBool("Run", m_player.m_isSprinting);
        }
    }

    
    public void f_updateUI() // update the UI to reflect the current ammo count
    {
        m_ammoCount.text = (m_currentAmmo.ToString() + "/" + m_maxAmmo.ToString());
    }
}
