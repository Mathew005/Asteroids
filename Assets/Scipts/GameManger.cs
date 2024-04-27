using UnityEngine;

public class GameManger : MonoBehaviour
{
    public Player player;
    public ParticleSystem explotion;
    public float respawnTime = 3.0f;
    public int lives = 3;
    public float respawnInvulnerabilityTime = 3.0f;
    public int score = 0;

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explotion.transform.position = asteroid.transform.position;
        this.explotion.Play();

        if( asteroid.size < 0.75)
        {
            score += 100;
        }else if( asteroid.size < 1.2f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }

    public void PlayerDied()
    {
        this.explotion.transform.position = player.transform.position;
        this.explotion.Play();

        this.lives--;

        if(this.lives <= 0)
        {
            GameOver();
        }
        else
        {
        Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn() {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.Invoke(nameof(TrunOnCollisions), this.respawnInvulnerabilityTime);
    }

    private void TrunOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        this.lives = 3;
        this.score = 0;

        Invoke(nameof(Respawn), this.respawnTime);
    }
}
