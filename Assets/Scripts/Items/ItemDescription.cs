using UnityEngine;
using UnityEngine.UI; // Required for UI components
using UnityEngine.EventSystems; // Required for event handling
using System.Collections.Generic; // Required for Dictionary

public class ItemDescription : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    public Text descriptionText; // Reference to the Unity UI Text component
    public PotionInHand potionInHand;

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
    { "bearPoop", "Bear poop, despite its unpleasant nature, is known to have various uses in wilderness survival, including as a natural fertilizer." },
    { "CatcallingPotion", "The Catcalling Potion is a mischievous concoction that, when consumed, causes the drinker to emit random catcalls involuntarily." },
    { "ConfusionPotion", "The Confusion Potion creates a cloud of bewilderment, causing those affected to lose their sense of direction and clarity." },
    { "FrostPotion", "The Frost Potion envelops the drinker in a chilling aura, freezing anything they touch and providing protection against heat." },
    { "HealingPotion", "The Healing Potion is a staple of any adventurer's kit, rapidly mending wounds and restoring vitality when consumed." },
    { "HolyGrailPotion", "The Holy Grail Potion is a legendary elixir rumored to grant eternal life and unparalleled wisdom to those who drink it." },
    { "InvisibilityPotion", "The Invisibility Potion renders the drinker invisible to the naked eye, making them undetectable for a short period." },
    { "LevitationPotion", "The Levitation Potion allows the drinker to float above the ground, defying gravity and gliding effortlessly through the air." },
    { "LiquidFlamePotion", "The Liquid Flame Potion sets the drinker's body ablaze without harm, enabling them to ignite objects and enemies with a touch." },
    { "LowerSusPotion", "The Lower Sus Potion reduces the drinker's suspiciousness, making them less likely to be suspected or detected by others." },
    { "MightPotion", "The Might Potion grants the drinker immense strength, enabling them to perform feats of physical power beyond normal capabilities." },
    { "ParalyticGasPotion", "The Paralytic Gas Potion releases a cloud of gas that paralyzes anyone who inhales it, rendering them immobile for a duration." },
    { "PurificationPotion", "The Purification Potion cleanses the body of toxins and curses, restoring purity and health to the drinker." },
    { "RecallPotion", "The Recall Potion transports the drinker to a previously marked location, providing a quick escape or return to a safe haven." },
    { "StrengthPotion", "The Strength Potion temporarily boosts the drinker's physical power, making them stronger and more formidable in combat." },
    { "SwiftnessPotion", "The Swiftness Potion enhances the drinker's speed and agility, allowing them to move and react with lightning-fast reflexes." },
    { "ToxicGasPotion", "The Toxic Gas Potion emits a poisonous cloud that damages and weakens anyone who breathes it in, making it a dangerous weapon." },
    { "UsefulnessPotion", "The Usefulness Potion makes the drinker exceptionally helpful and resourceful, able to find solutions to almost any problem." },
    { "UselessnessPotion", "The Uselessness Potion has the opposite effect, rendering the drinker incapable of performing even the simplest tasks effectively." },
    { "RepairPotion", "The Repair Potion restores broken items to their original condition, mending cracks and damages as if they were never there." },
    { "WeaknessPotion", "The Weakness Potion saps the strength and vitality of the drinker, leaving them feeble and vulnerable for a period of time." }
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

            if(spriteName.Contains("Potion")){
                potionInHand.potionName = spriteName;
            }
            
            // Check if the sprite name exists in the dictionary
            if (spriteDescriptions.ContainsKey(spriteName))
            {
                // Set the Text component text to the corresponding description
                descriptionText.text = spriteDescriptions[spriteName];
            }
        }
    }
}
