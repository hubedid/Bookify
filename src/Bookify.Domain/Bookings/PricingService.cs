﻿using Bookify.Domain.Apartments;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Bookings
{
    public class PricingService
    {
         public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
        {
            var curency = apartment.Price.Currency;

            var priceForPeriod = new Money(
                apartment.Price.Amount * period.LengthInDays,
                curency);

            decimal percentageUpCharge = 0;
            foreach(var amenity in apartment.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MountainView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                };
            }

            var amenitiesUpCharge = Money.Zero(curency);
            if(percentageUpCharge > 0)
            {
                amenitiesUpCharge = new Money(
                    priceForPeriod.Amount * percentageUpCharge, 
                    curency);   
            }

            var totalPrice = Money.Zero();

            totalPrice += priceForPeriod;

            if (!apartment.CleaningFee.IsZero())
            {
                totalPrice += apartment.CleaningFee;
            }

            totalPrice += amenitiesUpCharge;

            return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
        }
    }
}
