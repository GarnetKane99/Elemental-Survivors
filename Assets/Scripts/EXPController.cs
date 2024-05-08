using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPController : MonoBehaviour
{
    int xp;
    public void Init(int xpGain)
    {
        xp = xpGain;
    }
    void Update()
    {
        if (GameManager.instance.pauseFromUpgrade) { return; }

        //transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 1 * Time.deltaTime * 20);
        transform.Rotate(0, 0, 20 * Time.deltaTime);

        if (Vector2.Distance(transform.position, Controller.playerInstance.transform.position) < EXPManager.instance.pickupRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, Controller.playerInstance.transform.position, 5f * Time.deltaTime); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") { return; }

        collision.GetComponent<Controller>().IncreaseXP(xp == default ? 1 : xp);

        Destroy(this.gameObject);
    }
}
