using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    //SCRIPT PARA ATIVAR AS MAGIAS 

    public MagicStats magic;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform EnemyPos;


    [Header("          Components Book")]
    public static int PorcenInstantKill =30;
    public static bool getBook;
    [SerializeField] private float SetTimeBook = 1f;
    private float GetTimeBook;


    [Header("          Components Unity")]

    [SerializeField] private Animator anim;

 
    
    public void MagicAttack()
    {
      

        GameObject clone = Instantiate(magic.MagicPrefab, new Vector3(EnemyPos.position.x, EnemyPos.position.y + 5, 0), Quaternion.identity);

        Destroy(clone, 10f);

        
    }


    private void Update()
    {
        Magics();
    }

    public void Magics()
    {
        if (GetTimeBook <= 0)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                enemies = GameObject.FindGameObjectsWithTag("Enemy");

                GetTimeBook = SetTimeBook;

                if (!getBook)
                {
                    ActivateTransformCat();
                }
                else if (getBook)
                {
                    getBook = false;
                    anim.SetBool("Livro", false);
                    FindObjectOfType<CatController>().TransformCatAnimDisable();
                }
            }
        }
        else if (GetTimeBook > 0) GetTimeBook -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0) && getBook)
        {
            

            foreach (GameObject enemy in enemies)
            {
                

                var porcentagemDaVida = enemy.gameObject.GetComponent<EnemyController>().stats.MaxLife * ((double)PorcenInstantKill / 100);

                Debug.Log(porcentagemDaVida);

                if (enemy.gameObject.GetComponent<EnemyController>().GetLife() <= porcentagemDaVida)
                {
                    EnemyPos = enemy.transform;

                    anim.SetBool("Magic1", true);


                }
            }
            
        }
    }

    private void ActivateTransformCat()
    {
        FindObjectOfType<CatController>().TransformCatAnimActivate();
    }
    public void GetBook()
    {
        getBook = true;

    }
    public void GetBookAnimActivate()
    {
        anim.SetBool("Livro", true);
    }
    public void Magia1Active()
    {
        FindObjectOfType<MagicController>().MagicAttack();
    }
    public void Magia1Disable()
    {
        anim.SetBool("Magic1", false);
    }
}
