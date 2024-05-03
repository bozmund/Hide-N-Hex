using UnityEngine;

public class PlantDisapears : MonoBehaviour
{
    private int firebloom = 0;
    private int icecap = 0;
    private int sorrowmoss = 0;
    private int blindweed = 0;
    private int sungrass = 0;
    private int earthroot = 0;
    private int fadeleaf = 0;
    private int rotberry = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the player collides with an object, check the object's tag or name
        if (collision.gameObject.CompareTag("DisappearObject"))
        {
            if (collision.gameObject.name == "FirebloomPlant(Clone)")
            {
                // Increment the collision counter
                firebloom++;
            }

            else if(collision.gameObject.name == "IcecapPlant(Clone)")
            {
                icecap++;
            }

            else if (collision.gameObject.name == "SorrowmossPlant(Clone)")
            {
                sorrowmoss++;
            }

            else if (collision.gameObject.name == "BlindweedPlant(Clone)")
            {
                blindweed++;
            }

            else if (collision.gameObject.name == "SungrassPlant(Clone)")
            {
                sungrass++;
            }

            else if (collision.gameObject.name == "EarthrootPlant(Clone)")
            {
                earthroot++;
            }

            else if (collision.gameObject.name == "FadeleafPlant(Clone)")
            {
                fadeleaf++;
            }

            else if (collision.gameObject.name == "RotberryPlant(Clone)") //make basic else later but for now like this
            {
                rotberry++;
            }

            // Log the current count and the name of the collided object to the console
            Debug.Log($"Collision with '{collision.gameObject.name}'. Hej total collisions with 'DisappearObject': {firebloom}");

            // Make the object disappear
            Destroy(collision.gameObject);
        }
    }
}
