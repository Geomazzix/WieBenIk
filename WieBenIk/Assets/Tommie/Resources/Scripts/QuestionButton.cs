using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : MonoBehaviour {

    [SerializeField]
    private Animator options;
    [SerializeField]
    private Animator options2;
    [SerializeField]
    private Animator options3;

    public void OpenOptions() {
        options.SetBool("Menu_Out", !options.GetBool("Menu_Out"));
        options2.SetBool("Menu_Out", !options2.GetBool("Menu_Out"));
        options3.SetBool("Menu_Out", !options3.GetBool("Menu_Out"));
    }
}
