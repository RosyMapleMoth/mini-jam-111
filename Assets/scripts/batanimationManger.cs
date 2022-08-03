using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batanimationManger : MonoBehaviour
{
    public BoxCollider batHitbox;
    public void startSwing()
    {
        batHitbox.enabled = true;
    }

    public void endSwing()
    {
        batHitbox.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
