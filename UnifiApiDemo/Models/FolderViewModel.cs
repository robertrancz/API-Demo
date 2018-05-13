using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UnifiApiDemo.Models
{
    internal class FolderViewModel
    {
        [JsonProperty("text")]
        public string Text { get; internal set; }

        public Guid Id { get; internal set; }

        public Guid? ParentId { get; internal set; }

        public object Checked { get; internal set; }

        [JsonProperty("nodes")]
        public List<FolderViewModel> Children { get; internal set; }

        [JsonProperty("icon")]
        public string Icon { get; set; } = "glyphicons glyphicons-folder-minus";

        [JsonProperty("selectedIcon")]
        public string SelectedIcon { get; set; } = "glyphicons glyphicons-folder-plus";
    }
}