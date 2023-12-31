using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackPool : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject aim;
    [SerializeField]
    private GameObject[] attacks;
    [SerializeField]
    private List<GameObject> gameObjects;
    [SerializeField]
    private int currentAttackIndex = 0;
    [SerializeField]
    private float attackResetTimer = 1f;
    private float attackTime = 0.3f;
    private float lastAttackTime = -10f;
    void Start()
    {

        for (int i = 0; i < attacks.Length; i++)
        {
            GameObject attack = attacks[i];
            gameObjects.Add(Instantiate(attack));
            gameObjects[i].transform.SetParent(transform);
            gameObjects[i].SetActive(false);
        }

        aim = GameObject.FindWithTag("Aim");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && Time.time > attackTime + lastAttackTime)
        {
            if (Time.time > attackResetTimer + lastAttackTime)
            {
                currentAttackIndex = 0;
            }
            lastAttackTime = Time.time;
            StartCoroutine(executeAttack());
        }
    }
    IEnumerator executeAttack()
    {
     

        gameObjects[currentAttackIndex].transform.position = aim.transform.position;
        gameObjects[currentAttackIndex].transform.rotation = aim.transform.rotation;

        gameObjects[currentAttackIndex].SetActive(true);
    
        yield return new WaitForSeconds(attackTime);
        gameObjects[currentAttackIndex].SetActive(false);
        if (++currentAttackIndex == 3)
        {
            currentAttackIndex = 0;
        }

    }
}
