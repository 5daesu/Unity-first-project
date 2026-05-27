using System.Collections;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed;
    public float acceleration;
    public float damageRadius = 0.75f;
    public Sprite[] explosionSprites;
    public float explosionFrameDuration = 0.06f;

    float curSpeed;
    bool atkType;
    int atkDamage;
    bool isExploding;
    Sprite meteorSprite;
    SpriteRenderer spriteRenderer;
    Coroutine explosionCoroutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) meteorSprite = spriteRenderer.sprite;
    }

    public void Initialize(Vector3 targetPosition, bool atkType, int atkDamage)
    {
        this.targetPosition = targetPosition;
        this.atkType = atkType;
        this.atkDamage = atkDamage;
        transform.position = targetPosition + new Vector3(0, 5f, 0);
        ResetMeteor();
    }

    void OnEnable()
    {
        ResetMeteor();
    }

    void Update()
    {
        if (isExploding) return;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, curSpeed * Time.deltaTime);
        curSpeed += acceleration;
        CheckArrival();
    }

    void CheckArrival()
    {
        if (Vector3.Distance(gameObject.transform.position, targetPosition) <= 0.01f)
        {
            ApplyDamage();
            PlayExplosion();
        }
    }

    void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPosition, damageRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.tag != "Enemy") continue;

            MonsterBehavior monsterBehavior = hit.GetComponent<MonsterBehavior>();
            if (monsterBehavior != null) monsterBehavior.GetDamage(atkType, atkDamage);
        }
    }

    void ResetMeteor()
    {
        curSpeed = speed;
        isExploding = false;

        if (explosionCoroutine != null)
        {
            StopCoroutine(explosionCoroutine);
            explosionCoroutine = null;
        }

        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && meteorSprite != null) spriteRenderer.sprite = meteorSprite;
    }

    void PlayExplosion()
    {
        isExploding = true;
        transform.position = targetPosition;

        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || explosionSprites == null || explosionSprites.Length == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        explosionCoroutine = StartCoroutine(PlayExplosionCoroutine());
    }

    IEnumerator PlayExplosionCoroutine()
    {
        foreach (Sprite explosionSprite in explosionSprites)
        {
            if (explosionSprite != null) spriteRenderer.sprite = explosionSprite;
            yield return new WaitForSeconds(explosionFrameDuration);
        }

        explosionCoroutine = null;
        gameObject.SetActive(false);
    }
}
