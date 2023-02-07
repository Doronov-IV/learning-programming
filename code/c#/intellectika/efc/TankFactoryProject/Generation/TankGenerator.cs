using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainEntityProject.Generation
{
    public class TankGenerator
    {


        #region State


        private string workingDirectory = @"..\..\..\Data\Text\";


        private List<string>? ManufacturerNameList = null;


        private List<string>? EngineNameList = null;


        private List<string>? GunNameList = null;


        private List<string>? CountryList = null;


        private List<string>? TankNameList = null;



        #endregion State





        #region API


        public async Task<MainBattleTank> GetNext()
        {
            Random random = new();

            if (!FullyInitialized())
            {
                await FillAssetLists();
            }

            int nEngineManufacturerName = random.Next(0, ManufacturerNameList.Count);
            int nGunManufacturerName = random.Next(0, ManufacturerNameList.Count);
            int nTankManufacturerName = random.Next(0, ManufacturerNameList.Count);

            int nEngineName = random.Next(0, EngineNameList.Count);
            int nGunName = random.Next(0, GunNameList.Count);
            int nTankName = random.Next(0, TankNameList.Count);

            int nEngineManufacturerCountryName = random.Next(0, CountryList.Count);
            int nGuneManufacturerCountryNameName = random.Next(0, CountryList.Count);
            int nTankeManufacturerCountryNameName = random.Next(0, CountryList.Count);

            List<string> currencies = new()
            {
                "$",
                "€",
                "¥",
                "c$",
                "₽",
                "CN¥",
                "MXN"
            };

            Manufacturer gunManufacturer = null;
            Manufacturer engineManufacturer = null;
                gunManufacturer = new Manufacturer(ManufacturerNameList[nGunManufacturerName], CountryList[nGuneManufacturerCountryNameName], new Budget(random.Next(200_000, 2_000_000), currencies[random.Next(0, currencies.Count)]));
                engineManufacturer = new Manufacturer(ManufacturerNameList[nEngineManufacturerName], CountryList[nEngineManufacturerCountryName], new Budget(random.Next(200_000, 2_000_000), currencies[random.Next(0, currencies.Count)]));
            return new MainBattleTank(
                modelName: TankNameList[nTankName],
                crewCount: random.Next(3,4 + 1),
                manufactorerReference: gunManufacturer,
                priceReference: new Price(random.Next(2_000_000, 20_000_000), currencies[random.Next(0, currencies.Count)]),
                engineReference: new Engine(EngineNameList[nEngineName], random.Next(250,2000), engineManufacturer, new Price(random.Next(200_000, 2_000_000), currencies[random.Next(0, currencies.Count)])),
                gunReference: new Gun(GunNameList[nGunName], random.Next(70,152), random.Next(20,60), gunManufacturer, new Price(random.Next(200_000, 2_000_000), currencies[random.Next(0, currencies.Count)]))
            );
        }


        #endregion API





        #region Logic



        private async Task FillAssetLists()
        {
            var arrayManufacturers = await File.ReadAllLinesAsync(workingDirectory + "Manufacturers.txt");
            var arrayEngines = await File.ReadAllLinesAsync(workingDirectory + "Engines.txt");
            var arrayGuns = await File.ReadAllLinesAsync(workingDirectory + "Guns.txt");
            var arrayCountries = await File.ReadAllLinesAsync(workingDirectory + "Countries.txt");
            var arrayTankName = await File.ReadAllLinesAsync(workingDirectory + "TankNames.txt");

            ManufacturerNameList = arrayManufacturers.ToList();
            EngineNameList = arrayEngines.ToList();
            GunNameList = arrayGuns.ToList();
            CountryList = arrayCountries.ToList();
            TankNameList = arrayTankName.ToList();
        }


        private bool FullyInitialized()
        {
            return ManufacturerNameList is not null && EngineNameList is not null && GunNameList is not null && CountryList is not null && TankNameList is not null;
        }



        #endregion Logic





        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TankGenerator()
        {

        }


        #endregion Construction


    }
}
