//Daniel Hiroshi Fugikawa - 200209
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Declaração da variavel de lista
    private List<Transform> _segments = new List<Transform>();
    //Declaração da variavel de transform de seguimento
    public Transform segmentPrefab;
    //Declaração do vetor de direção
    public Vector2 direction = Vector2.right;
    //Declaração do tamanho inicial da cobra
    public int initialSize = 4;
        
    //Inicio do jogo
    private void Start()
    {
        //Chamada da função que reinicia a posição da cobra e seus segimentos
        ResetState();
    }
    
    //Mantem uma atualização constante nos comandos
    private void Update()
    {
        //Comando que muda a direção da cobra verticalmente
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //Comando que muda a direção da cobra horizontalmente
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        //Definindo os seguimentos do corpo da cobra
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //Define a posição dos seguimentos do corpo da cobra
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }
    
    //Definindo a coletavel
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //Reinicia a posição e estado da cobra
    public void ResetState()
    {
        //Faz com q a cobra volte a sua posição inicial e andando para a direita
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //Destroir os seguimentos
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //Alterar a quantidade de seguimentos 
        _segments.Clear();
        _segments.Add(this.transform);

        //Aumentar a cobre a cada comida coletada
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Defenindo o collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Caso pegue uma comida a cobra cresce
        if (other.tag == "Food") {
            Grow();
        } 
        //Caso bata no obstaculo ele reinicia o jogo
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
