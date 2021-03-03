using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{
    [Range(1, 15)]
    public float velocidade = 5.0f;
    public int space = 0;
    private Vector3 direcao;
    GameManager gm;
    public GameObject heart1, heart2, heart3;
    
    void Start()
    {
        
        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);
        direcao = new Vector3(dirX, dirY).normalized;
        


        gm = GameManager.GetInstance();

        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (Input.GetKey("space") && space == 0) {
            transform.position += direcao * Time.deltaTime * velocidade;
            space = 1;

        }

        if (space == 1) {
            transform.position += direcao * Time.deltaTime * velocidade;
        }
        
       

        Vector2 posicaoViewport = Camera.main.WorldToViewportPoint(transform.position);

        // Fazer com que a bola nao trave no canto
        if( posicaoViewport.x < 0 || posicaoViewport.x > 1 )
        {
            direcao = new Vector3(-direcao.x, direcao.y);
        }

        if( posicaoViewport.y < 0 || posicaoViewport.y > 1 )
        {
            direcao = new Vector3(direcao.x, -direcao.y);
        }
        if(posicaoViewport.y < 0){
            if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
            {
                gm.ChangeState(GameManager.GameState.ENDGAME);
            }    
            Reset(); 
        }
        //Debug
        Debug.Log($"Vidas: {gm.vidas} \t | \t Pontos: {gm.pontos}");
        
        
    }

    private void Reset(){
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0, 0.5f, 0);

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(2.0f, 5.0f);

        direcao = new Vector3(dirX, dirY).normalized;

        gm.vidas--;

        switch(gm.vidas){
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.gameObject.CompareTag("Player")){
            float dirX = Random.Range(-5.0f, 5.0f);
            float dirY = Random.Range(1.0f, 5.0f);
            GetComponent<AudioSource>().Play();
            direcao = new Vector3(dirX, dirY).normalized;

        }
        else if(col.gameObject.CompareTag("Tijolo")) {
            direcao = new Vector3(direcao.x, -direcao.y);
            GetComponent<AudioSource>().Play();
            gm.pontos++;
        }


        


    }
    
}
