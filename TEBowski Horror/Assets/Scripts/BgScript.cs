using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    public static BgScript BgInstance;      //Tworzymy instancje muzyki.

    private void Awake()
    {
        if(BgInstance != null && BgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        BgInstance = this;      //Tworzymy loop tej instancji, a mówiąc po polsku dajemy żeby nie znikała po zmianie sceny.
        DontDestroyOnLoad(this);
    }
}
