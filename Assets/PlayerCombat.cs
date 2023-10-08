using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    // Attacking variables
    public Transform attackPoint;
    public LayerMask enemyLayers; 
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 4;
    float nextAttackTime = 0;
    
    // Health variables 
    public Slider slider;
    public GameObject sliderObject;
    static public float maxHealth = 200;
    static public float currentHealthPlayer;
    public Text HealthText;
    public GameObject HealthTextObject;

    // Potion variables + Debuff
    int numberOfHealthPotions = 0;
    public int Healthtimer = 60;
    public Text HealthPotionNumber;
    public Text HealthTimerText;
    public GameObject HealthTimerObject;
    public bool CanTakeHealthPotion = true;
    int numberOfAttackPotions = 0;
    int Attacktimer = 60;
    public Text AttackPotionNumber;
    public Text AttackTimerText;
    public GameObject AttackTimerObject;
    public bool CanTakeAttackPotion = true;
    int Shieldtimer = 60;
    int numberOfShieldPotions = 0;
    public Text ShieldPotionNumber;
    public Text ShieldTimerText;
    public GameObject ShieldTimerObject;
    public bool CanTakeShieldPotion = true;
    int Debufftimer = 10;
    public Text DebuffTimerText;
    public GameObject DebuffTimerObject;
    public bool PlayerIsDebuffed;

    // Skills variables
    public bool HasChosenSkill1 = false;
    public bool HasChosenSkill2 = false;
    public GameObject firePoint;
    public GameObject Swords;
    int swords;
    public bool SwordsThrown = false;
    public GameObject TerraBeam;
    public bool TerraBeamThrown = false;
    public bool CanPlayerDodge = false;
    public bool CanPlayerRegenerate = false;
    public bool IsRegenerating = false;
    public GameObject JesterArrow;
    public bool JesterArrowToEnemy = false;
    public GameObject Orb;
    int Orbtimer = 40;
    public GameObject OrbTimerObject;
    public Text OrbTimerText;
    public bool OrbCanBeActivated = false;
    public bool OrbIsActivated = false;
    public bool immuneToDebufs = false;
    public bool fasterTimer = false;
    public bool secondLife = false;

    // Sound effects variables
    public AudioSource attackSound;

    // Other variables
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Sprite PlayerIdle;
    public Sprite WebbedPlayer;
    public GameObject DeathScene;

    // Slider colors
    Color blue = new Color(62f/255f, 197f/255f, 229f/255f);
    Color yellow = new Color(222f/255f, 177f/255f, 28f/255f);
    Color red = new Color(222f/255f, 28f/255f, 28f/255f);

    void Start()
    {
        currentHealthPlayer =  maxHealth;
        slider.value = GetHealth();
        SetHealthText();
    }

    void Update()
    {
        slider.value = GetHealth();
        SetHealthText();

        if (Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                swords = 0;
            } 
        }

        if(CanPlayerRegenerate && !IsRegenerating && currentHealthPlayer != maxHealth)
        {
            RegenerateHealth();
        }
    
        if(OrbCanBeActivated)
        {
            if(Input.GetKeyDown(KeyCode.K) && !OrbIsActivated)
            {
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = blue;
                Orb.SetActive(true);
                OrbIsActivated = true;
                OrbCanBeActivated = false;
                HealthText.color = blue;
                Invoke("RemoveOrb", 10);
            }
        }

        if(immuneToDebufs)
        {
            Debufftimer = 1;
        }

    }

    private void SetHealthText()
    {
        HealthText.text = currentHealthPlayer.ToString();
    }

    void Attack() 
    {
        animator.SetTrigger("Attack");
        attackSound.Play();
        
        if(HasChosenSkill1 && !SwordsThrown)
        {
            Instantiate(Swords, firePoint.transform.position, transform.rotation);
            Invoke("Attack", 0.2f);
            swords += 1;

            if(swords == 5)
            {
                SwordsThrown = true;
                Invoke("WaitTime", 2);
            }
            
        } else if(HasChosenSkill2 && !TerraBeamThrown)
        {
            Instantiate(TerraBeam, firePoint.transform.position, transform.rotation);
            TerraBeamThrown = true;
            Invoke("WaitTime", 2);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        } 
    }

    public void WaitTime()
    {
        SwordsThrown = false;
        TerraBeamThrown = false;
    }

    public void TakeDamage(int damage)
    {
        if(CanPlayerDodge)
        {
            System.Random rnd = new System.Random();
            int num = rnd.Next(10);

            if(num == 1)
            {
                Debug.Log("Dodged attack");
            } else {
                ChangeDamage(damage);
            }
        } else {
            ChangeDamage(damage);
        }

        if(JesterArrowToEnemy)
        {
            Instantiate(JesterArrow, transform.position, transform.rotation);
        }

        if (currentHealthPlayer <= 0)
        {
            if (secondLife == true)
            {
                currentHealthPlayer = maxHealth;
                secondLife = false;
                // revival message
            } else {
                HealthTextObject.SetActive(false);
                sliderObject.SetActive(false);
                gameObject.SetActive(false);
                DeathScene.SetActive(true);
            }
        }
    }

    void ChangeDamage(int damage)
    {
        if (OrbIsActivated)
        {
            damage = 0;
        } else if (!CanTakeShieldPotion) {
            if(damage <= 10)
            {
                currentHealthPlayer -= 1;
            } else{
                currentHealthPlayer -= (damage - 10);
            }  
        } else {
              currentHealthPlayer -= damage;
        }
    }

    public bool UpdateHealth(int health)
    {
        if (currentHealthPlayer == maxHealth) 
        {
            return false;
        }
        if (currentHealthPlayer + health > maxHealth)
        {
            currentHealthPlayer = maxHealth;
        }
        else
        {
            currentHealthPlayer += health;
        }
        return true;
    } 

    public void RemoveOrb()
    {
        Orb.SetActive(false);
        OrbTimerObject.SetActive(true);
        ActivateOrbTimer();
        OrbIsActivated = false;  
        slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
        HealthText.color = red;
    }

    public void ActivateOrbTimer()
    {
        Orbtimer -= 1;
        OrbTimerText.text = Orbtimer.ToString();

        if (Orbtimer == 0)
        {
            OrbTimerObject.SetActive(false);
            OrbCanBeActivated = true;
            Orbtimer = 40;
            OrbTimerText.text = "40";
        } else 
        {
            Invoke("ActivateOrbTimer", 1);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void UpdateAttackPotion(int number)
    {
        numberOfAttackPotions += number;
        AttackPotionNumber.text = numberOfAttackPotions.ToString();
    }

    public void UpdateHealthPotion(int number)
    {
        numberOfHealthPotions += number;
        HealthPotionNumber.text = numberOfHealthPotions.ToString();
    }

    public void UpdateShieldPotion(int number)
    {
        numberOfShieldPotions += number;
        ShieldPotionNumber.text = numberOfShieldPotions.ToString();
    }

    public int GetAttackPotionNumber()
    {
        return numberOfAttackPotions;
    }

    public int GetHealthPotionNumber()
    {
        return numberOfHealthPotions;
    }

    public int GetShieldPotionNumber()
    {
        return numberOfShieldPotions;
    }

    public void UpdateDamage ()
    {
        attackDamage += 20;
        AttackTimerObject.SetActive(true);
        CanTakeAttackPotion = false;

        Invoke("ChangeAttackTimer", 1);
    }

    public void ChangeAttackTimer()
    {
        Attacktimer -= 1;
        AttackTimerText.text = Attacktimer.ToString();

        if (Attacktimer == 0)
        {
            AttackTimerObject.SetActive(false);
            AttackTimerText.text = "60";
            attackDamage -= 20;
            CanTakeAttackPotion = true;
            Attacktimer = 60;
        } else 
        {
            Invoke("ChangeAttackTimer", 1);
        }
    }

    public bool UpdateHealthViaPotion(int health)
    {
        if (currentHealthPlayer == maxHealth) 
        {
            return false;
        }
        if (currentHealthPlayer + health > maxHealth)
        {
            currentHealthPlayer = maxHealth;
            SetHealthText();
            CanTakeHealthPotion = false;
            HealthTimerObject.SetActive(true);

            Invoke("ChangeHealthTimer", .1f);
        }
        else
        {
            currentHealthPlayer += health;
            SetHealthText();
            CanTakeHealthPotion = false;
            HealthTimerObject.SetActive(true);

            Invoke("ChangeHealthTimer", .1f);
        }
        return true;
    } 

    public void ChangeHealthTimer()
    {
        HealthTimerText.text = Healthtimer.ToString();
        Healthtimer -= 1;

        if (Healthtimer == 0)
        {
            HealthTimerObject.SetActive(false);
            CanTakeHealthPotion = true;
            if (fasterTimer) {
                Healthtimer = 45;
            } else {
                Healthtimer = 60;
            }
        } else 
        {
            Invoke("ChangeHealthTimer", 1);
        }
    }    

    public void ShieldPotion()
    {
        ShieldTimerObject.SetActive(true);
        CanTakeShieldPotion = false;

        Invoke("ChangeShieldTimer", 1);
    }

    public void ChangeShieldTimer()
    {
        Shieldtimer -= 1;
        ShieldTimerText.text = Shieldtimer.ToString();

        if (Shieldtimer == 0)
        {
            ShieldTimerObject.SetActive(false);
            ShieldTimerText.text = "60";
            CanTakeShieldPotion = true;
            Shieldtimer = 60;
        } else 
        {
            Invoke("ChangeShieldTimer", 1);
        } 
    }

    public bool GetCanTakeAttackPotion()
    {
        return CanTakeAttackPotion;
    }

    public bool GetCanTakeHealthPotion()
    {
        return CanTakeHealthPotion;
    }

    public bool GetCanTakeShieldPotion()
    {
        return CanTakeShieldPotion;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        gameObject.SetActive(true);
        DeathScene.SetActive(false);
        PlayerData data = SaveSystem.LoadPlayer();
        Debug.Log(data.amountOfShieldPotions);

        currentHealthPlayer = data.playerHealth;
        numberOfAttackPotions = data.amountOfAttackPotions;
        numberOfHealthPotions = data.amountOfHealthPotions;
        numberOfShieldPotions = data.amountOfShieldPotions;
        AttackPotionNumber.text = numberOfAttackPotions.ToString();
        HealthPotionNumber.text = numberOfHealthPotions.ToString();
        ShieldPotionNumber.text = numberOfShieldPotions.ToString();

        Vector3 newPosition;
        newPosition.x = data.playerPosition[0];
        newPosition.y = data.playerPosition[1];
        newPosition.z = data.playerPosition[2];
        transform.position = newPosition;
    }

    public void FreezePlayer()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        PlayerMovement instance = GameObject.Find("Player").GetComponent<PlayerMovement>();
        instance.runSpeed = 0f;
        CharacterController2D instance2 = GameObject.Find("Player").GetComponent<CharacterController2D>();
        instance2.m_JumpForce = 0f;
        spriteRenderer.sprite = WebbedPlayer; 
        Invoke("UnfreezePlayer", 2);
    }

    public void UnfreezePlayer()
    {
        PlayerMovement instance = GameObject.Find("Player").GetComponent<PlayerMovement>();
        instance.runSpeed = 40f;          
        CharacterController2D instance2 = GameObject.Find("Player").GetComponent<CharacterController2D>();
        instance2.m_JumpForce = 800f;
        spriteRenderer.sprite = PlayerIdle; 
        gameObject.GetComponent<Animator>().enabled = true;
    }

    public void DebuffPlayer()
    {
        if(!immuneToDebufs)
        {
            PlayerIsDebuffed = true;
            DebuffTimerObject.SetActive(true);
            Invoke("ChangeDebuffTimer", 1);
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = yellow;
            HealthText.color = yellow;
        }
    }

    public void ChangeDebuffTimer()
    {
        Debufftimer -= 1;
        DebuffTimerText.text = Debufftimer.ToString();
        TakeDamage(1);

        if (Debufftimer == 0)
        {
            DebuffTimerObject.SetActive(false);
            DebuffTimerText.text = "10";
            Debufftimer = 10;
            PlayerIsDebuffed = false;
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = red;
            HealthText.color = red;
        } else 
        {
            Invoke("ChangeDebuffTimer", 1);
        }
    }

    public void ResetDebuffTimer()
    {
        Debufftimer = 10;
    }

    float GetHealth()
    {
        return currentHealthPlayer / maxHealth;
    }


    public void RegenerateHealth()
    {
        if(currentHealthPlayer == maxHealth)
        {
            IsRegenerating = false;
        } else if (!PlayerIsDebuffed) {
            IsRegenerating = true;
            currentHealthPlayer += 1;
            SetHealthText();
            Invoke("RegenerateHealth", 2);
        }
    }

}