using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EntityController : MonoBehaviour
{
    CinemachineVirtualCamera _cvc;

    [SerializeField] GameObject _crashPrefab = null;
    [SerializeField] GameObject _slimePrefab = null;
    [SerializeField] GameObject _trooperPrefab = null;


    [Header("Offsets")]
    [SerializeField] Vector2 _slimeOffset = new Vector2(-3, 1);
    [SerializeField] Vector2 _trooperOffset = new Vector2(5, 1);

    //state
    
    GameObject _crash;
    List<GameObject> _slimeList = new List<GameObject>();
    List<GameObject> _trooperList = new List<GameObject>();

    private void Start()
    {
        _cvc = Camera.main.GetComponentInChildren<CinemachineVirtualCamera>();
    }


    #region Spawn Entities

    public void SpawnCrash()
    {
        if (_crash)
        {
            Debug.Log("There is already a Crash!");
        }
        else
        {
            GameObject go = Instantiate(_crashPrefab, new Vector2(0, 1), Quaternion.identity);
            _crash = go;
            _cvc.Follow = _crash.transform;
        }
    }

    public void SpawnSlime()
    {

    }

    public void SpawnTrooper()
    {

    }

    #endregion

}
