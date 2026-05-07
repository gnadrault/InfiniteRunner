using UnityEngine;

namespace Feedback
{
    public class ExplosionForce : MonoBehaviour
    {
        public float explosionForce = 50f;

        public float desagregationSpeed;

        //Cr�er un effet d'explosion pour que les balles partent a des endroits diff�rents
        private void Start()
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * explosionForce, ForceMode2D.Impulse);
        }

        //R�duit progressivement leur taille, quand atteinds proche de 0, d�truit les objets
        private void Update()
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * desagregationSpeed);

            if(transform.localScale.magnitude < 0.05f)
            {
                Destroy(gameObject);
            }
        }
    }
}
