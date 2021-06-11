using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonController : MonoBehaviour
{
    //Public Variables for customization
    public float fireCooldown = 1f;
    public GameObject Projectile;
    //Public Variables
    public GameObject HarpoonG; //The one held by GunArm
    public GameObject HarpoonF; //The one held by LeftArm
    public GameObject GunArm; //Posed for aiming weapon straight ahead
    public GameObject LeftArm; //The regular arm
    public Transform projectileSpawn;
    public Sprite LoadedHarpoon;
    public Sprite UnloadedHarpoon;

    //Private Variables
    private Animator animController;
    private bool holdingF;
    private float fireCooldownTimer;
    private bool canFire;
    

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        holdingF = false;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingF && canFire)
        {
            StartCoroutine(FireWeapon());
        }
    }

    public void SetHoldingF(bool input)
    {
        holdingF = input;
        if(holdingF == true)
        {
            GunArm.SetActive(true);
            LeftArm.SetActive(false);
        }
        else
        {
            GunArm.SetActive(false);
            LeftArm.SetActive(true);
        }
    }

    private IEnumerator FireWeapon()
    {
        canFire = false;
        animController.SetTrigger("Fire");
        StartCoroutine(FireProjectile());
        HarpoonG.GetComponent<SpriteRenderer>().sprite = UnloadedHarpoon;
        HarpoonF.GetComponent<SpriteRenderer>().sprite = UnloadedHarpoon;
        yield return new WaitForSeconds(fireCooldown);
        HarpoonG.GetComponent<SpriteRenderer>().sprite = LoadedHarpoon;
        HarpoonF.GetComponent<SpriteRenderer>().sprite = LoadedHarpoon;
        yield return new WaitForSeconds(0.2f);
        canFire = true;
    }

    private IEnumerator FireProjectile()
    {
        GameObject newProjectile = Instantiate(Projectile, projectileSpawn.position, Quaternion.identity);
        if(transform.localScale.x < 0f)
        {
            newProjectile.transform.localScale = new Vector3(newProjectile.transform.localScale .x * - 1, newProjectile.transform.localScale.y * 1, 1);
        }
        yield return new WaitForSeconds(3f);
        GameObject.Destroy(newProjectile);

    }
}
