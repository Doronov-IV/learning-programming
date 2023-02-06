namespace MainEntityProject.Data.JSON
{
    public static class TankJsonSerializer
    {

        private static string jsonDirectoryName = @$"..\..\..\Data\JSON\Tanks\";


        private static int fileId = 1;


        private static object locker = new();


        public static async Task Serialize(MainBattleTank vehicle)
        {
            string jsonFileName = GetFileName(vehicle);
            string alternativeJsonFileName = jsonDirectoryName + $"unnamed{fileId}.json";

            if (!File.Exists(jsonFileName))
            {
                lock (locker)
                {
                    string jsonString = JsonConvert.SerializeObject(vehicle, Formatting.Indented);

                    try
                    {
                        Task.Run(() =>
                        {
                            File.Create(jsonFileName);
                        });
                    }
                    catch (System.IO.IOException ex)
                    {
                        jsonFileName = alternativeJsonFileName;
                        fileId++;
                    }

                    File.WriteAllTextAsync(jsonFileName, jsonString);
                }
            }
        }


        public static async Task<MainBattleTank?> DeserializeOne(string modelName)
        {
            MainBattleTank? vehicle = null;

            string jsonFileName = GetFileName(modelName);

            if (File.Exists(jsonFileName))
            {
                lock (locker)
                {
                    string jsonString = File.ReadAllText(jsonFileName);

                    vehicle = JsonConvert.DeserializeObject<MainBattleTank>(jsonString);
                }
            }
            else throw new FileNotFoundException($@"[Manual] file {modelName} was not found in the json directory.");

            return vehicle;
        }


        public static async Task<List<MainBattleTank>> DeserializeAll()
        {
            List<MainBattleTank> result = new();

            if (Directory.Exists(jsonDirectoryName))
            {
                DirectoryInfo dir = new (jsonDirectoryName);

                foreach (FileInfo file in dir.GetFiles())
                {
                    MainBattleTank? mbt = await DeserializeOne(file.Name);

                    if (mbt is not null) result.Add(mbt);
                }
            } else throw new DirectoryNotFoundException($@"[Manual] the directory, hardcoded in the tank parser, was not found.");

            return result;
        }


        private static string GetFileName(MainBattleTank vehicle)
        {
            return jsonDirectoryName + vehicle.ModelName + ".json";
        }


        private static string GetFileName(string vehicleModelName)
        {
            return jsonDirectoryName + vehicleModelName + ".json";
        }

    }
}
