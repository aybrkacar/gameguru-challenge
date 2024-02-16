using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PlatformParentController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Renderer _basePlatform;
    [SerializeField] private LeanGameObjectPool _pool;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            _pool.Spawn();
        }
    }
}
