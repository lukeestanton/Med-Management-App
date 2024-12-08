using MedManagementLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _physicianRoot;
        private Filebase _instance;
        public Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }
                return _instance;
            }
        }
        private Filebase()
        {
            // Get the path to the user's home directory
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Define the root directory as the Downloads folder
            _root = Path.Combine(homeDirectory, "Downloads");

            // Define the Physicians subdirectory within Downloads
            _physicianRoot = Path.Combine(_root, "Physicians");

            // Ensure that the directories exist
            Directory.CreateDirectory(_physicianRoot);
        }
        public int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Select(x => x.Id).Max();
                }
                return 0;
            }
        }
        public Physician AddOrUpdate(Physician physician)
        {
            //set up a new Id if one doesn't already exist
            if(physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
            }
            //go to the right place
            string path = $"{_physicianRoot}\\{physician.Id}.json";
            
            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(physician));
            //return the item, which now has an id
            return physician;
        }
        
        public List<Physician> Physicians
        {
            get
            {
                var root = new DirectoryInfo(_physicianRoot);
                var _physicians = new List<Physician>();
                foreach(var physicianFile in root.GetFiles())
                {
                    var physician = JsonConvert
                        .DeserializeObject<Physician>
                        (File.ReadAllText(physicianFile.FullName));
                    if(physician != null)
                    {
                        _physicians.Add(physician);
                    }
                }
                return _physicians;
            }
        }
        public bool Delete(string type, string id)
        {
            //TODO: refer to AddOrUpdate for an idea of how you can implement this.
            return true;
        }
    }
   
}
