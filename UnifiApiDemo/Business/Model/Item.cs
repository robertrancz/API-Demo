using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifiApiDemo.Business.Model
{
    public enum ItemType
    {
        Unknown,
        Analysis,
        SampleSet,
        SampleData,
        SampleResult,
        SampleList,
        Report,
        AnalysisMethod,
        FileData,
        ReportTemplate,
        PrintData
    }

    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ItemType Type { get; set; }
    }
}
