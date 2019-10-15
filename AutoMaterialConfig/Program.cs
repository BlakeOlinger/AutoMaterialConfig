using SldWorks;
using SwConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMaterialConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var swInstance = new SldWorks.SldWorks();

            var model = (ModelDoc2)swInstance.ActiveDoc;

            // read rebuild.txt to get the config.txt file path
            var rebuildAppDataPath = @"C:\Users\bolinger\Documents\SolidWorks Projects\Prefab Blob - Cover Blob\app data\rebuild.txt";
            var configPath = @System.IO.File.ReadAllLines(rebuildAppDataPath)[0];

            // get material value from config.txt file
            var configLines = System.IO.File.ReadAllLines(configPath);
            var material = "";
            foreach(string line in configLines)
            {
                if (line.Contains("Material"))
                {
                    material = line.Split('=')[1].Trim();
                }
            }

            // populate material dictionary - by hand for now
            var materialDict = new Dictionary<string, string>();
            materialDict.Add("0", "ASTM A36 Steel");
            materialDict.Add("1", "6061 Alloy");

            // allow user to select material via a variable in config.txt file for *.SLDPRT files - intended for covers only atm
            var partDoc = (PartDoc)model;
            var materialDatabases = swInstance.GetMaterialDatabases();
            var db = (string)materialDatabases[3];

            // set material property on active doc - DOES NOT currently check to see if the appropriate doc is currently active
            partDoc.SetMaterialPropertyName(db, materialDict[material]);
        }
    }
}
