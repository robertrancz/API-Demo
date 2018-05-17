//***************************************************************************************
// Copyright © 2015 Waters Corporation. All Rights Reserved.
//
//***************************************************************************************

using System.Collections.Generic;

namespace UnifiApiDemo.Business.Model
{
    public class SampleSet : ItemBase
    {
        #region Properties to be filled when the model is retrieved

        //--- nothing here at the moment

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        //public IEnumerable<Analysis> Analyses { get; set; }
        public IEnumerable<SampleData> SampleData { get; set; }

        #endregion
    }
}
