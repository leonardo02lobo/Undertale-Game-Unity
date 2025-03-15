using UnityEngine;

public class SonidoEntreE : MonoBehaviour
{
    private SonidoEntreE instace;
    public SonidoEntreE Instance
    {
        get
        {
            return instace;
        }
    }

    private void Awake()
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if(instace != null && instace != this)
        {
            Destroy(gameObject );
            return;
        }
        else
        {
            instace = this;
        }
        DontDestroyOnLoad(gameObject);  
    }
}
