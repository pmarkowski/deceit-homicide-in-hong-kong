namespace Deceit.Domain.Game.SceneCards;

class LocationOfCrimeSceneCards
{
    public static IEnumerable<SceneCard> Cards =>
        new List<SceneCard>()
        {
                new SceneCard(
                    "Location of Crime",
                    new List<string>()
                    {
                        "Living Room",
                        "Bedroom",
                        "Storeroom",
                        "Bathroom",
                        "Kitchen",
                        "Balcony"
                    }
                ),
                new SceneCard(
                    "Location of Crime",
                    new List<string>()
                    {
                        "Vacation Home",
                        "Park",
                        "Supermarket",
                        "School",
                        "Woods",
                        "Bank"
                    }
                ),
                new SceneCard(
                    "Location of Crime",
                    new List<string>()
                    {
                        "Pub",
                        "Bookstore",
                        "Restaurant",
                        "Hotel",
                        "Hospital",
                        "Building Site"
                    }
                ),
                new SceneCard(
                    "Location of Crime",
                    new List<string>()
                    {
                        "Playground",
                        "Classroom",
                        "Dormitory",
                        "Cafeteria",
                        "Elevator",
                        "Toilet"
                    }
                )
        };
}
