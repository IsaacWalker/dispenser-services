using System;
using System.Collections.Generic;
using System.Text;

namespace Web.EntityData
{
    /// <summary>
    /// Frequency of a prescription
    /// </summary>
    public enum Frequency
    {
        DAILY,
        OTHER_DAY, // Every other day
        BID, // twice a day
        TID, // three times a day
        QID, // four times a day
        QHS, // every bedtime
        Q3H, // every 3 hours
        Q4H, //..
        Q5H, // ..
        QWK // every week
    }
}
