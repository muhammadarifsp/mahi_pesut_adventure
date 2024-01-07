using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed = 20;
    private int desiredLane = 1; // 0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;
    static public bool isStarted = false;
    public float jumpForce;
    public float gravity = -20f;
    public Animator animator;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public bool isGrounded;
    public GameObject gameManager;
    public GameObject charModel;
    public AudioSource deathFX;
    private string playerName;
    private string gameMode;
    public static List<bool> booster = new List<bool> { false, false, false }; //0:Score, 1:Coin, 2:Immune
    private List<string> boosterName = new List<string> { "Score", "Coin", "Immune" };
    public GameObject immuneEffect;
    private bool isImmune;
    public float boosterTimeout = 5f;
    public static float boosterDelay;
    private AudioSource boosterFX;

    private void Awake()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Anonymous");
        gameMode = PlayerPrefs.GetString("GameMode");
        boosterDelay = boosterTimeout;

        boosterFX = GameObject.Find("BoosterFX").GetComponent<AudioSource>() as AudioSource;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ketika booster list ada yg true
        int indexBooster = booster.IndexOf(true);
        if (indexBooster != -1)
        {
            // matikan booster yg true
            booster[indexBooster] = false;

            switch (indexBooster)
            {
                // index 0 skor booster
                case 0:
                    StartCoroutine(ScoreTimesTwo());
                    break;
                // index 1 koin booster
                case 1:
                    StartCoroutine(CoinTimesTwo());
                    break;
                // index 2 immune booster
                case 2:
                    StartCoroutine(Immune());
                    break;
                default:
                    break;
            }
        }

        direction.z = forwardSpeed; // move forward
        // transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed, Space.World);

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isStarted ? isGrounded : true);

        if (isGrounded && isStarted)
        {
            direction.y = -1;
            // jump controll
            if (Input.GetKeyDown(KeyCode.W) || SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        // if game is not started yet then character cant be move
        if (!isStarted)
            return;

        if (Input.GetKeyDown(KeyCode.S) || SwipeManager.swipeDown)
        {
            if (!isGrounded)
            {
                direction.y = -jumpForce;
            }
            StartCoroutine(Slide());
        }

        if (forwardSpeed < maxSpeed) // up speed
            forwardSpeed += 0.25f * Time.deltaTime;

        //Move controll left & right
        if ((Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight) && desiredLane < 2)
        {
            desiredLane++;
        }
        if ((Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft) && desiredLane > 0)
        {
            desiredLane--;
        }

        // set position target as center for the default position
        Vector3 targetPosition =
            transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0) // move position to left
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2) // move position to right
        {
            targetPosition += Vector3.right * laneDistance;
        }

        // set character position as the position we already set before
        // transform.position = Vector3.Lerp(
        //     transform.position,
        //     targetPosition,
        //     60 * Time.fixedDeltaTime
        // );

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.fixedDeltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        }
        else
        {
            controller.Move(diff);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //  Debug.Log(hit.transform.tag);
        if (hit.transform.tag == "Booster")
        {
            // booster logic
            string[] bsName = hit.transform.gameObject.name.Split("_"); // ambil nama object yg di collect
            // perulangan sesuai banyaknya booster
            for (int i = 0; i < boosterName.Count; i++)
            {
                // jika nama object yg di hit player sesuai sama nama booster
                if (boosterName[i] == bsName[0])
                {
                    booster[i] = true; // boosternya jadi true
                }
            }

            boosterFX.Play();
            Destroy(hit.transform.gameObject);

            // jika booster immune active
            if (bsName[0] == boosterName[2])
            {
                BoosterController.boosterImmune = true;
            }
        }

        if (hit.transform.tag == "Obstacle")
        {
            if (isImmune)
            {
                Destroy(hit.transform.gameObject);
                // hit.transform.gameObject.SetActive(false);
            }
            else
            {
                isStarted = false; // reset is the game started

                this.gameObject.GetComponent<PlayerController>().enabled = false; // disabled script
                charModel.GetComponent<Animator>().Play("Death"); // play death character animation
                ScoreController.isEnded = true; // set bool for stop counting score
                deathFX.Play(); // play sfx for death
                gameManager.GetComponent<GameOver>().enabled = true; // enable script for game over event
                TileGenerator.isEnded = true; // stop generate tile

                // jika game mode free run, skor akhir masuk ke highscore
                if (gameMode == "FreeRun")
                {
                    // add to highscore
                    int score = ScoreController.scoreCount;
                    MenuHighscores.AddHighscoreEntry(playerName, score);
                    // add total score untuk buka track ke 2
                    int lastTotalScore = PlayerPrefs.GetInt("TotalScore", 0);
                    int totalScore = lastTotalScore + score;
                    PlayerPrefs.SetInt("TotalScore", totalScore);
                }

                // save coin
                int coinSaved = PlayerPrefs.GetInt("TotalCoin", 0); // ambil coin disimpan
                int coinCollected = CollectableController.coinCount; // ambil coin yang diperoleh
                PlayerPrefs.SetInt("TotalCoin", coinSaved + coinCollected); // jumlahkan dan simpan
            }
        }
    }

    IEnumerator Slide()
    {
        animator.SetBool("isSliding", true);

        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
    }

    IEnumerator Immune()
    {
        // nyalakan immune
        // Debug.Log("Immune Aktif");
        immuneEffect.SetActive(true);
        isImmune = true;

        // setelah 3 detik
        yield return new WaitForSeconds(boosterDelay);

        // matikan immune
        // Debug.Log("Immune Non Aktif");
        immuneEffect.SetActive(false);
        isImmune = false;
    }

    IEnumerator ScoreTimesTwo()
    {
        // Debug.Log("Skor X 2 Aktif");
        ScoreController.isBoosted = true;

        // setelah 3 detik
        yield return new WaitForSeconds(boosterDelay);

        ScoreController.isBoosted = false;
        // Debug.Log("Skor X 2 Non Aktif");
    }

    IEnumerator CoinTimesTwo()
    {
        // Debug.Log("Koin X 2 Aktif");
        CollectCoin.isBoosted = true;

        // setelah 3 detik
        yield return new WaitForSeconds(boosterDelay);

        CollectCoin.isBoosted = false;
        // Debug.Log("Koin X 2 Non Aktif");
    }
}
