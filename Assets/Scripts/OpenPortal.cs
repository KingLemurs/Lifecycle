using UnityEngine;
using System.Collections;

public class OpenPortal : MonoBehaviour
{
    public GameObject portal;
    public float timeToSpawn;
    public GameObject sandbox;

    private void Start()
    {
        StartCoroutine(SpawnPortal(timeToSpawn));
    }

    private IEnumerator SpawnPortal(float timeToSpawn)
    {
        yield return new WaitForSeconds(timeToSpawn);
        portal.SetActive(true);
        sandbox.GetComponent<Outline>().enabled = true;
    }    
}
