using UnityEngine;

public class ComboEspecial : MonoBehaviour
{
    public Animator animator;
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("Desaparecer", true);
        }
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("Desaparecer", false);
        }
    }
}