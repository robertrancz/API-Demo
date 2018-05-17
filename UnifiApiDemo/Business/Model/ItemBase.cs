using System;

namespace UnifiApiDemo.Business.Model
{
    public abstract class ItemBase
    {
        #region Properties to be filled when the model is retrieved

        public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

        #endregion

        #region Properties NOT to be filled when the model is retrieved

        public Folder Folder { get; set; }

        #endregion
    }
}
