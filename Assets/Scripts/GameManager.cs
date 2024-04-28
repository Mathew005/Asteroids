﻿using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject startGameUI;
    [SerializeField] private GameObject HUD;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    public AudioSource shipExplosionAudio;
    public AudioSource rockExplosionAudio;

    private int score;
    private int lives;

    public int Score => score;
    public int Lives => lives;

    private GameObject gameUI;
    private bool gameState = false;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        startGameUI.SetActive(true);
        gameOverUI.SetActive(false);
        HUD.SetActive(false);
        player.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!gameState && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            gameState = true;
            startGameUI.SetActive(false);
            HUD.SetActive(true);
            NewGame();
        }
        if (lives <= 0 && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))) {
            NewGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        Respawn();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    private void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void OnAsteroidDestroyed(Asteroid asteroid)
    {
        explosionEffect.transform.position = asteroid.transform.position;
        explosionEffect.Play();

        if (asteroid.size < 0.7f) {
            SetScore(score + 100); // small asteroid
            rockExplosionAudio.volume = 0.25f;
            rockExplosionAudio.Play();
        } else if (asteroid.size < 1.4f) {
            SetScore(score + 50); // medium asteroid
            rockExplosionAudio.volume = 0.50f;
            rockExplosionAudio.Play();
        } else {
            SetScore(score + 25); // large asteroid
            rockExplosionAudio.volume = 1.0f;
            rockExplosionAudio.Play();
        }
    }

    public void OnPlayerDeath(Player player)
    {
        shipExplosionAudio.Play();
        player.gameObject.SetActive(false);

        explosionEffect.transform.position = player.transform.position;
        explosionEffect.Play();

        SetLives(lives - 1);

        if (lives <= 0) {
            gameOverUI.SetActive(true);
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }

}
