using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Models
{
    public class ConfirmAdministrationModel
    {
        /// <summary>
        /// ODF Id
        /// </summary>
        public Guid OdfId { get; set; }


        /// <summary>
        /// Time of administration
        /// </summary>
        public DateTime AdministrationTime { get; set; }
    }
}
