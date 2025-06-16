using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject player;
    Character playerCharacter;
    [SerializeField] GameObject enemy;
    Character enemyCharacter;

    [SerializeField] Image playerSpriteDisplay;
    [SerializeField] Image playerHealthDisplay;
    [SerializeField] Image enemySpriteDisplay;
    [SerializeField] Image enemyHealthDisplay;

    private void Awake()
    {
        if (player == null) { Debug.LogError("Player was not provided"); return; }
        if (enemy == null) { Debug.LogError("Enemy was not provided"); return; }
        
        playerCharacter = Instantiate(player).GetComponent<Character>();
        if(playerCharacter == null) { Debug.LogError("Player game object is not a character"); return; }
        enemyCharacter= Instantiate(enemy).GetComponent<Character>();
        if(enemyCharacter == null) { Debug.LogError("Enemy game object is not a character"); return; }

        //Player set up
        playerSpriteDisplay.sprite = playerCharacter.Sprite;
        playerHealthDisplay.fillAmount = enemyCharacter.HealthPrecentage;

        //Enemy set up
        enemySpriteDisplay.sprite = enemyCharacter.Sprite;
        enemyHealthDisplay.fillAmount = enemyCharacter.HealthPrecentage;
    }
}
