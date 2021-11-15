using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Chamada da função que aleatoriza a posição das frutas
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Cria a comida na área
        Bounds bounds = this.gridArea.bounds;

        //Aleatoriza os valores de x e y dentro dos limites da area do jogo
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //Definindo valores de X e Y
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Quando a outra comida for coletada cria-se outra
        RandomizePosition();
    }

}
