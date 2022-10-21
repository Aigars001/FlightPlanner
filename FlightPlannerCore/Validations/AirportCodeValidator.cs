using FlightPlannerCore.Models;
using System;
using System.Collections.Generic;
namespace FlightPlannerCore.Validations
{
    public class AirportCodeValidator :  IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.AirportCode);
        }
    }
}
