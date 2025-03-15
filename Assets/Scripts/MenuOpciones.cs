using UnityEngine;
using UnityEngine.Audio;

public class MenuOpciones : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioBehaviour;
    public void CambiarVOlumen(float volumen)
    {
        AudioBehaviour.SetFloat("Volumen",volumen);
    }
}
