public class GardenMap
{
    public GardenComponent DestinationComponent { get; set; }
    public GardenComponent SourceComponent { get; set; }
    public List<MapSet> Maps { get; set; } = [];



    public GardenMap(string mapDefinition)
    {
        string[] items = mapDefinition.Split(' ')[0].Split("-to-");
        SourceComponent = ToGardenComponent(items[0]);
        DestinationComponent = ToGardenComponent(items[1]);
    }


    GardenComponent ToGardenComponent(string component) => component switch
    {
        "seed" => GardenComponent.Seed,
        "soil" => GardenComponent.Soil,
        "fertilizer" => GardenComponent.Fertilizer,
        "water" => GardenComponent.Water,
        "light" => GardenComponent.Light,
        "temperature" => GardenComponent.Temperature,
        "humidity" => GardenComponent.Humidity,
        "location" => GardenComponent.Location,
        _ => GardenComponent.Unknown
    };
}