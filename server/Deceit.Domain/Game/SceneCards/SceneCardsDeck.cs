namespace Deceit.Domain.Game.SceneCards;

class SceneCardsDeck : Deck<SceneCard>
{
    public SceneCardsDeck() : base(
        new List<SceneCard>()
        {
                new SceneCard(
                    "Motive of Crime",
                    new List<string>()
                    {
                        "Hatred",
                        "Power",
                        "Money",
                        "Love",
                        "Jealousy",
                        "Justice",
                    }),
                new SceneCard(
                    "Corpse Condition",
                    new List<string>()
                    {
                        "Still Warm",
                        "Stiff",
                        "Decayed",
                        "Incomplete",
                        "Intact",
                        "Twisted",
                    }),
                new SceneCard(
                    "Victim's Build",
                    new List<string>()
                    {
                        "Large",
                        "Thin",
                        "Tall",
                        "Short",
                        "Disfigured",
                        "Fit",
                    }),
                new SceneCard(
                    "Time of Death",
                    new List<string>()
                    {
                        "Dawn",
                        "Morning",
                        "Noon",
                        "Afternoon",
                        "Evening",
                        "Midnight",
                    }),
                new SceneCard(
                    "Social Relationship",
                    new List<string>()
                    {
                        "Relatives",
                        "Friends",
                        "Colleagues",
                        "Employer / Employee",
                        "Lovers",
                        "Strangers",
                    }),
                new SceneCard(
                    "Day of Crime",
                    new List<string>()
                    {
                        "Weekday",
                        "Weekend",
                        "Spring",
                        "Summer",
                        "Autumn",
                        "Winter",
                    }),
                new SceneCard(
                    "Weather",
                    new List<string>()
                    {
                        "Sunny",
                        "Stormy",
                        "Dry",
                        "Humid",
                        "Cold",
                        "Hot",
                }),
                new SceneCard(
                    "Victim's Identity",
                    new List<string>()
                    {
                        "Child",
                        "Young Adult",
                        "Middle - Aged",
                        "Senior",
                        "Male",
                        "Female",
                    }),
                new SceneCard(
                    "Victim's Clothes",
                    new List<string>()
                    {
                        "Neat",
                        "Untidy",
                        "Elegant",
                        "Shabby",
                        "Bizarre",
                        "Naked",
                    }),
                new SceneCard(
                    "Duration of Crime",
                    new List<string>()
                    {
                        "Instanteous",
                        "Brief",
                        "Gradual",
                        "Prolonged",
                        "Few Days",
                        "Unclear",
                    }),
                new SceneCard(
                    "Victim's Occupation",
                    new List<string>()
                    {
                        "Boss",
                        "Professional",
                        "Worker",
                        "Student",
                        "Unemployed",
                        "Retired",
                    }),
                new SceneCard(
                    "Hint on Corpse",
                    new List<string>()
                    {
                        "Head",
                        "Chest",
                        "Hand",
                        "Leg",
                        "Partial",
                        "All-over",
                    }),
                new SceneCard(
                    "Murderer's Personality",
                    new List<string>()
                    {
                        "Arrogant",
                        "Despicable",
                        "Furious",
                        "Greedy",
                        "Forceful",
                        "Perverted",
                    }),
                new SceneCard(
                    "Evidence Left Behind",
                    new List<string>()
                    {
                        "Natural",
                        "Artistic",
                        "Written",
                        "Synthetic",
                        "Personal",
                        "Unrelated",
                    }),
                new SceneCard(
                    "Trace at the Scene",
                    new List<string>()
                    {
                        "Fingerprint",
                        "Footprint",
                        "Bruise",
                        "Blood Stain",
                        "Body Fluid",
                        "Scar",
                    }),
                new SceneCard(
                    "In Progress",
                    new List<string>()
                    {
                        "Entertainment",
                        "Relaxation",
                        "Assembly",
                        "Trading",
                        "Visit",
                        "Dining",
                    }),
                new SceneCard(
                    "General Impression",
                    new List<string>()
                    {
                        "Common",
                        "Creative",
                        "Fishy",
                        "Cruel",
                        "Horrible",
                        "Suspenseful",
                    }),
                new SceneCard(
                    "State of The Scene",
                    new List<string>()
                    {
                        "Bits and Pieces",
                        "Ashes",
                        "Water Stain",
                        "Cracked",
                        "Disorderly",
                        "Tidy",
                    }),
                new SceneCard(
                    "Victim's Expression",
                    new List<string>()
                    {
                        "Peaceful",
                        "Struggling",
                        "Frightened",
                        "In Pain",
                        "Blank",
                        "Angry",
                    }),
                new SceneCard(
                    "Noticed by Bystander",
                    new List<string>()
                    {
                        "Sudden sound",
                        "Prolonged sound",
                        "Smell",
                        "Visual",
                        "Action",
                        "Nothing",
                    }),
                new SceneCard(
                    "Sudden Incident",
                    new List<string>()
                    {
                        "Power Failure",
                        "Fire",
                        "Conflict",
                        "Loss of Valuables",
                        "Scream",
                        "Nothing",
                    }),
        })
    {
    }
}

