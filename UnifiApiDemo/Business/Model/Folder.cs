using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifiApiDemo.Business.Model
{
    public enum FolderType
    {
        Unknown,
        Database,
        Department,
        Lab,
        Project
    }

    public class Folder
    {
        public Folder()
        {
            Subfolders = new List<Folder>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Path { get; set; }

        public FolderType FolderType { get; set; }

        public Guid? ParentId { get; set; }

        public List<Folder> Subfolders { get; set; }
    }
}
