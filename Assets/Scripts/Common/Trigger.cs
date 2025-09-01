using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public SceneType startScene;
    
    void Start()
    {
        SceneLoadManager.Instance.LoadScene(startScene);
    }
}
