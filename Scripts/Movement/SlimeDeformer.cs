using UnityEngine;

/*public class JellyDeformer : MonoBehaviour
{
    [Header("Jelly Settings")]
    public float stiffness = 12f;          // жёсткость пружины
    public float damping = 0.82f;           // затухание
    public float maxDeform = 0.4f;          // максимум деформации
    public float impactMultiplier = 0.08f;  // влияние скорости

    Vector2 currentScale = Vector2.one;
    Vector2 targetScale = Vector2.one;
    Vector2 velocity;

    float currentRotation;
    float targetRotation;
    float rotationVelocity;


    void Start()
    {
        targetScale = new Vector2(1.5f, 0.6f);
    }

    void Update()
    {
        // scale spring
        velocity += (targetScale - currentScale) * stiffness * Time.deltaTime;
        velocity *= damping;
        currentScale += velocity * Time.deltaTime;

        // rotation spring (чтобы деформация шла по направлению удара)
        rotationVelocity += (targetRotation - currentRotation) * stiffness * Time.deltaTime;
        rotationVelocity *= damping;
        currentRotation += rotationVelocity * Time.deltaTime;

        transform.localScale = new Vector3(currentScale.x, currentScale.y, 1f);
        transform.localRotation = Quaternion.Euler(0, 0, currentRotation);

        // возврат к норме
        targetScale = Vector2.one;
        targetRotation = 0f;
    }

    public void Deform(Vector2 impactNormal, float impactSpeed)
    {
        Debug.Log("JELLY DEFORM CALLED! speed = " + impactSpeed);
        float deformAmount = Mathf.Clamp(
            impactSpeed * impactMultiplier,
            0f,
            maxDeform
        );

        // направление деформации
        Vector2 normal = impactNormal.normalized;

        // угол поворота деформации
        targetRotation = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

        // сжатие вдоль удара, растяжение поперёк
        float squash = 1f - deformAmount;
        float stretch = 1f + deformAmount;

        targetScale = new Vector2(stretch, squash);
    }
}*/



/*public class JellyDeformer : MonoBehaviour
{
    [Header("Jelly")]
    public float stiffness = 12f;
    public float damping = 0.82f;

    Vector2 currentScale = Vector2.one;
    Vector2 velocity;

    float currentRotation;
    float rotationVelocity;

    Vector2 targetScale = Vector2.one;
    float targetRotation = 0f;

    void Update()
    {
        // scale spring
        velocity += (targetScale - currentScale) * stiffness * Time.deltaTime;
        velocity *= damping;
        currentScale += velocity * Time.deltaTime;

        // rotation spring
        rotationVelocity += (targetRotation - currentRotation) * stiffness * Time.deltaTime;
        rotationVelocity *= damping;
        currentRotation += rotationVelocity * Time.deltaTime;

        transform.localScale = new Vector3(currentScale.x, currentScale.y, 1f);
        transform.localRotation = Quaternion.Euler(0, 0, currentRotation);

        // 🔁 МЕДЛЕННО возвращаем цель к норме
        targetScale = Vector2.Lerp(targetScale, Vector2.one, Time.deltaTime * 4f);
        targetRotation = Mathf.Lerp(targetRotation, 0f, Time.deltaTime * 4f);
    }

    public void Deform(Vector2 impactNormal, float impactSpeed)
    {
        float deform = Mathf.Clamp(impactSpeed * 0.08f, 0f, 0.5f);

        Vector2 n = impactNormal.normalized;

        targetRotation = Mathf.Atan2(n.y, n.x) * Mathf.Rad2Deg;

        targetScale = new Vector2(
            1f + deform, // stretch
            1f - deform  // squash
        );
    }
}*/

public class JellyDeformer : MonoBehaviour
{
    public float stiffness = 12f;
    public float damping = 0.85f;
    public float maxDeformation = 0.35f;
    
    public float jitterStrength = 0.04f;
    public float jitterFrequency = 18f;
    float jitterTimer;

    private Vector2 velocity;
    private Vector2 deformation;

    void Update()
    {
        // 1. Пружина (физика)
        velocity += -deformation * stiffness * Time.deltaTime;
        velocity *= damping;
        deformation += velocity * Time.deltaTime;

        deformation = Vector2.ClampMagnitude(deformation, maxDeformation);

        // 2. Джиттер (визуал)
        Vector2 jitterOffset = Vector2.zero;

        if (jitterTimer > 0f)
        {
            float jitter = Mathf.Sin(Time.time * jitterFrequency)
                           * jitterStrength
                           * jitterTimer;

            jitterOffset = new Vector2(jitter, -jitter);

            jitterTimer -= Time.deltaTime * 2f;
        }

        // 3. Итоговый scale
        Vector2 final = deformation + jitterOffset;

        transform.localScale = new Vector3(
            1f + final.x,
            1f + final.y,
            1f
        );
    }

    // 🔴 ВОТ ОН. ЕГО НУЖНО ДОБАВИТЬ
    public void ApplyImpact(Vector2 force)
    {
        //Debug.Log("Impact received: " + force);
        velocity += force;

        jitterTimer = Mathf.Clamp(jitterTimer + force.magnitude * 0.5f, 0f, 1f);
    }
}
