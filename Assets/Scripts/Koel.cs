using System;
using UnityEngine;

public class Koel : MonoBehaviour
{
    private const float SHIP_OVERFLOW_WIDTH = 0.3f;
    private const float SHIP_OVERFLOW_HEIGHT_TOP = 0.75f;
    private const float SHIP_OVERFLOW_HEIGHT_BOTTOM = 0.85f;
    private const float CUTSCENE_SHIP_SPEED = 12f;
    private const float CUSTCENE_SHIP_END_POS = -4.0f;

    private Vector3 originalShipPosition = new Vector3(0f, -13.2f, 0f);

    private SpriteRenderer shipSprite;
    public float shipSpeed;

    public GameConstants.Polarity shipPolarity;

    public Animation shipRotateAnimations;

    public GameConstants.GameEvents currentMotion;

    /*public float mag;
    public float x1;
    public float x2;
    public float y1;
    public float y2;
    public float x3;
    public float y3;*/

    public int health;
    public GameObject damageEffect;

    //private bool isShipInvul;
    private bool isShipInRotate;

    private Vector2 initialTouchPosition;

    public static event EventHandler KoelDied;

    public static Koel instance;

    public static Koel GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        currentMotion = GameConstants.GameEvents.PlayerEntryCutscene;

        transform.position = originalShipPosition;
        shipSprite = GetComponent<SpriteRenderer>();
        shipSprite.sprite = GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION.NONE);
        shipSpeed = 0.7f; //0.6 to 1

        shipPolarity = GameConstants.Polarity.light; //default polarity is light

        health = 3;
        //isShipInvul = false;
        isShipInRotate = false; //to avoid player to rotate ship instantly

        BossEvent.BossDied += Koel_BossExitEvent;

        /*mag = 10f;
        x1 = 0f;
        x2 = 0f;
        y1 = 0f;
        y2 = 0f;
        x3 = 0f;
        y3 = 0f;*/
    }

    private void OnDestroy()
    {
        BossEvent.BossDied -= Koel_BossExitEvent;
    }

    private void Update()
    {
        if (currentMotion == GameConstants.GameEvents.PlayerEntryCutscene)
        {
            transform.Translate(Vector2.up * Time.deltaTime * CUTSCENE_SHIP_SPEED);
            if (transform.position.y >= CUSTCENE_SHIP_END_POS)
            {
                currentMotion = GameConstants.GameEvents.PlayerControlling;
                LevelHandler
                    .GetInstance()
                    .AddEventToQueue(GameConstants.GameEvents.PreGameEvent1, 
                                     GameConstants.StartPreGameEvent1Tutorial, 2.0f);
            }
        }
        else
        {
            if (Input.touchCount > 0 && !LevelHandler.GetInstance().isGameOver)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    initialTouchPosition = Input.GetTouch(0).position;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 currentDeltaPosition = new Vector2(touch.position.x - initialTouchPosition.x,
                                                               touch.position.y - initialTouchPosition.y);

                    //float speed = GetSpeed(touch);
                    /*if (OnlyHorizontalMovement(touch.deltaPosition))
                    {
                        speed = SHIP_SPEED_HORIZONTAL;
                    }*/
                    /*if (currentDeltaPosition.magnitude > mag)
                    {
                        mag = currentDeltaPosition.magnitude;
                        x3 = currentDeltaPosition.x;
                        y3 = currentDeltaPosition.y;

                        x1 = initialTouchPosition.x;
                        x2 = touch.position.x;

                        y1 = initialTouchPosition.y;
                        y2 = touch.position.y;
                    }*/
                    initialTouchPosition = touch.position;

                    SetShipSpriteBasedOnSpeed(currentDeltaPosition);
                    Vector2 shipPosition = new Vector2(transform.position.x, transform.position.y);
                    //shipPosition += (touch.deltaPosition * Time.deltaTime * speed);
                    shipPosition += (currentDeltaPosition * Time.deltaTime * shipSpeed);
                    HandleShipOutOfBound(shipPosition);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary
                       || Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    shipSprite.sprite = GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION.NONE);
                }

                if (Input.touchCount == 2)
                {
                    if (Input.GetTouch(1).phase == TouchPhase.Began && !isShipInRotate)
                    {
                        //second tap on screen only
                        SwitchShipPolarity();
                    }
                }
            }
            else
            {
                shipSprite.sprite = GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION.NONE);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (ShouldPlayerTakeDamage(collider.gameObject))
        {
            //TODO: add bullets later
            health -= 1; //change to 0 or 1 for debugging purposes
            AudioHandler.PlayAudio(AudioHandler.Sound.PlayerDamage, 0.5f);
            damageEffect.SetActive(true);
            if (ShouldColliderBeDestroyed(collider.gameObject)) {
                Destroy(collider.gameObject);
            }
            shipRotateAnimations.Play("ShipDamageInvulBlinking"); //1 sec invul

            if (health == 2)
            {
                transform.Find("DamageParts").GetComponent<Animation>().Play("DamagePartLeft");
            }
            else if (health == 1)
            {
                transform.Find("DamageParts").GetComponent<Animation>().Play("DamagePartRight");
            }else if (health == 0)
            {
                LevelHandler.GetInstance().isGameOver = true;
                KoelDied?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private bool ShouldColliderBeDestroyed(GameObject gameObject)
    {
        if (gameObject.CompareTag("PlayerTakeDmgNoDestroy"))
        {
            return false;
        }
        return true;
    }


        private bool ShouldPlayerTakeDamage(GameObject gameObject)
    {
        if (gameObject.CompareTag("EnemyDark")
            || gameObject.CompareTag("EnemyLight")
            || gameObject.CompareTag("PlayerTakeDmgNoDestroy"))
        {
            return true;
        }

        if ((shipPolarity == GameConstants.Polarity.dark && gameObject.CompareTag("EnemyLightBullet"))
            || (shipPolarity == GameConstants.Polarity.light && gameObject.CompareTag("EnemyDarkBullet")))
        {
            return true;
        }

        return false;
    }

    public void Koel_BossExitEvent(object sender, EventArgs e)
    {
        DisableShipCollider(); //setting isShipInRotate to true is fine here
    }

    public void DisableShipCollider()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
        //isShipInvul = true;
        isShipInRotate = true;
    }

    public void EnableShipCollider()
    {
        transform.GetComponent<BoxCollider2D>().enabled = true;
        //isShipInvul = false;
    }

    public void ShipRotateStop()
    {
        isShipInRotate = false;
    }

    private void SwitchShipPolarity()
    {
        AudioHandler.PlayAudio(AudioHandler.Sound.SwitchPolarity, 0.5f);
        if (shipPolarity == GameConstants.Polarity.light)
        {
            shipRotateAnimations.Play("ShipRotateFromLightFromRight");

            Instantiate(GameAssets.GetInstance().lightToDarkParSys, transform.position, Quaternion.identity);
            shipPolarity = GameConstants.Polarity.dark;
        }
        else
        {
            shipRotateAnimations.Play("ShipRotateFromDarkFromRight");

            Instantiate(GameAssets.GetInstance().darkToLightParSys, transform.position, Quaternion.identity);
            shipPolarity = GameConstants.Polarity.light;
        }
    }

    private GameConstants.DIRECTION GetHorizontalMovementDir(Vector2 deltaPosition)
    {
        GameConstants.DIRECTION direction = GameConstants.DIRECTION.NONE;
        if (deltaPosition.x > 10.0f)
        {
            direction = GameConstants.DIRECTION.RIGHT;
        }
        else if(deltaPosition.x < -10.0f)
        {
            direction = GameConstants.DIRECTION.LEFT;
        }

        return direction;
    }

    /*private float GetSpeed(Touch touch)
    {
        float speed;
        float distance = touch.position.magnitude;
        if (touch.deltaTime == 0.0f)
        {
            speed = 0.0f;
        }
        else
        {
            speed = (distance / touch.deltaTime);
        }
        Debug.Log(speed);
        return speed;
    }*/

    private void HandleShipOutOfBound(Vector2 shipPosition)
    {
        if (FixShipPositionIfNotinBound(shipPosition))
        {
            transform.position = shipPosition;
        }
    }

    private bool FixShipPositionIfNotinBound(Vector2 shipPosition)
    {
        bool isInBound = true;
        if (!(shipPosition.x >= (GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH)))
        {
            //left side only
            transform.position = new Vector3(GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH, shipPosition.y, 0.0f);
            isInBound = false;
        }
        else if (!(shipPosition.x <= (GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH)))
        {
            //right side only
            transform.position = new Vector3(GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH, shipPosition.y, 0.0f);
            isInBound = false;
        }
        if (!(shipPosition.y <= (GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP)))
        {
            //up only
            transform.position = new Vector3(shipPosition.x, GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP, 0.0f);
            isInBound = false;
        }
        else if (!(shipPosition.y >= (GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM)))
        {
            //down only
            transform.position = new Vector3(shipPosition.x, GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM, 0.0f);
            isInBound = false;
        }
        if (!(shipPosition.x >= (GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH))
            && !(shipPosition.y <= (GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP)))
        {
            //left up corner
            transform.position = new Vector3(GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH,
                                     GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP, 0.0f);
            isInBound = false;
        }
        else if (!(shipPosition.x >= (GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH))
            && !(shipPosition.y >= (GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM)))
        {
            //left down corner
            transform.position = new Vector3(GameConstants.BORDER_LEFT + SHIP_OVERFLOW_WIDTH,
                                     GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM, 0.0f);
            isInBound = false;
        }
        if (!(shipPosition.x <= (GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH))
            && !(shipPosition.y <= (GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP)))
        {
            //right up corner
            transform.position = new Vector3(GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH,
                                     GameConstants.BORDER_UP - SHIP_OVERFLOW_HEIGHT_TOP, 0.0f);
            isInBound = false;
        }
        else if (!(shipPosition.x <= (GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH))
            && !(shipPosition.y >= (GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM)))
        {
            //right down corner
            transform.position = new Vector3(GameConstants.BORDER_RIGHT - SHIP_OVERFLOW_WIDTH,
                                     GameConstants.BORDER_DOWN + SHIP_OVERFLOW_HEIGHT_BOTTOM, 0.0f);
            isInBound = false;
        }
        return isInBound;
    }

    private void SetShipSpriteBasedOnSpeed(Vector2 deltaPosition)
    {
        if (GetHorizontalMovementDir(deltaPosition) == GameConstants.DIRECTION.LEFT)
        {
            //Debug.Log(speed);
            shipSprite.sprite = GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION.LEFT);



        }
        else if (GetHorizontalMovementDir(deltaPosition) == GameConstants.DIRECTION.RIGHT)
        {
            //Debug.Log(speed);
            shipSprite.sprite = GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION.RIGHT);



        }
    }

    private Sprite GetShipSpriteBasedOnPolarity(GameConstants.DIRECTION direction)
    {
        if (shipPolarity == GameConstants.Polarity.light) {
            if (direction == GameConstants.DIRECTION.RIGHT)
            {
                
                return GameAssets.GetInstance().shipTiltRightLight;
            }
            else if (direction == GameConstants.DIRECTION.LEFT)
            {
               return GameAssets.GetInstance().shipTiltLeftLight;
            }
            else
            {
                return GameAssets.GetInstance().shipNeutralLight;
            }
        }
        else
        {
            if (direction == GameConstants.DIRECTION.RIGHT)
            {
                return GameAssets.GetInstance().shipTiltRightDark;
            }
            else if (direction == GameConstants.DIRECTION.LEFT)
            {
                return GameAssets.GetInstance().shipTiltLeftDark;
            }
            else
            {
                return GameAssets.GetInstance().shipNeutralDark;
            }
        }
    }
}
