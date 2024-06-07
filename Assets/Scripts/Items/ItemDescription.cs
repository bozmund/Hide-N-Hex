using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.EventSystems; // Required for event handling
using System.Collections.Generic; // Required for Dictionary

public class ItemDescription : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    public Text descriptionText; // Reference to the Unity UI Text component

    // Dictionary to map sprite names to their descriptions
    private Dictionary<string, string> spriteDescriptions = new Dictionary<string, string>()
    {
            { "firebloom", "The Firebloom is a rare plant that glows with an inner flame. Its petals are hot to the touch, and it is said to thrive in volcanic soils." },
            { "icecap", "The Icecap is a delicate plant that blooms only in the coldest climates. Its leaves are perpetually covered in frost, making it a wonder of nature." },
            { "blandfruit", "Blandfruit is a plain-looking fruit with a mild flavor. It is often overlooked, but some believe it has subtle healing properties." },
            { "blindweed", "Blindweed is a creeping vine with dark leaves. It grows in shadowy places and is known for its ability to obscure vision when brewed into a potion." },
            { "dewcatcher", "Dewcatcher plants are renowned for their ability to collect and hold dew. Their large, cup-shaped leaves are always filled with crystal-clear water." },
            { "earthroot", "Earthroot is a sturdy plant that grows deep in the soil. Its roots are incredibly strong, anchoring it firmly in place even during the fiercest storms." },
            { "fadeleaf", "Fadeleaf is a mysterious plant that seems to disappear when not in direct sunlight. Its leaves are thin and translucent, giving it an ethereal appearance." },
            { "goldenLotus", "The Golden Lotus is a beautiful, radiant flower that is highly sought after for its brilliant yellow petals. It is often used in ceremonial garlands." },
            { "mageroyal", "Mageroyal is a plant that is prized by magicians and alchemists. Its vibrant purple flowers are said to enhance magical abilities when used in potions." },
            { "rotberry", "Rotberry is a small, red fruit that has a pungent odor. Despite its smell, it is used in many concoctions for its potent properties." },
            { "sorrowmoss", "Sorrowmoss grows in damp, gloomy areas. Its dark green moss is often used in rituals and is believed to absorb negative energy." },
            { "starflower", "Starflower blooms at night, with petals that reflect the starlight. Its beauty is unmatched, and it is often used in romantic bouquets." },
            { "stormvine", "Stormvine is a resilient plant that thrives during thunderstorms. Its twisting vines are said to conduct electricity, making it a favorite of storm mages." },
            { "sungrass", "Sungrass is a tall, golden plant that grows in open fields. It sways gently in the breeze, creating a sea of gold under the sunlight." },
            { "swiftthistle", "Swiftthistle is a spiky plant that grows rapidly. It is known for its sharp thorns and is often used in potions to increase agility." },
            { "AmberPotion", "The Amber Potion is a warm, glowing liquid that exudes a soothing fragrance. It is believed to calm the mind and body when consumed." },
            { "AzurePotion", "Azure Potion is a cool, blue concoction that sparkles with tiny bubbles. It is known to refresh and invigorate those who drink it." },
            { "BistrePotion", "Bistre Potion is a dark, earthy brew with a rich, smoky flavor. It is often used to ground one's senses and enhance focus." },
            { "CharcoalPotion", "Charcoal Potion is a pitch-black liquid that absorbs light. It is rumored to purify the body and spirit when taken in small doses." },
            { "CrimsonPotion", "Crimson Potion is a vibrant, red elixir with a spicy kick. It is said to energize and embolden the drinker." },
            { "GoldenPotion", "Golden Potion is a shimmering, gold liquid that glows softly. It is believed to bring good fortune and vitality to those who consume it." },
            { "IndigoPotion", "Indigo Potion is a deep, indigo-colored brew with a mystical aura. It is often used in rituals to enhance intuition and spiritual insight." },
            { "IvoryPotion", "Ivory Potion is a creamy, white liquid with a delicate aroma. It is said to cleanse the soul and promote inner peace." },
            { "JadePotion", "Jade Potion is a translucent, green elixir with a refreshing taste. It is known to rejuvenate the body and mind." },
            { "MagentaPotion", "Magenta Potion is a bright, magenta-colored liquid with a sweet, floral scent. It is believed to inspire creativity and passion." },
            { "SilverPotion", "Silver Potion is a glittering, silver brew that seems to shimmer in the light. It is said to enhance clarity and wisdom." },
            { "TurquoisePotion", "Turquoise Potion is a vibrant, turquoise-colored liquid with a tropical flavor. It is known to boost energy and lift the spirits." }
        };


    void Start()
    {
        // Get the Image component attached to this GameObject
        image = GetComponent<Image>();
    }

    // This method is called when the GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if the Image component and its sprite are not null
        if (image != null && image.sprite != null)
        {
            // Get the sprite name
            string spriteName = image.sprite.name;

            // Check if the sprite name exists in the dictionary
            if (spriteDescriptions.ContainsKey(spriteName))
            {
                // Set the Text component text to the corresponding description
                descriptionText.text = spriteDescriptions[spriteName];
            }
        }
    }
}
