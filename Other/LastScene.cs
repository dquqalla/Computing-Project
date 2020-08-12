using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("PartyPopperSFX");
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
    }
}
