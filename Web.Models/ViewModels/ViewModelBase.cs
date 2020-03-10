using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models.ViewModels
{
    public abstract class ViewModelBase
    {
        /// <summary>
        /// The nurse Id
        /// </summary>
        public Guid NurseId { get; set; }
    }
}
