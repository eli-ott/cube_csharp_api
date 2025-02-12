using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;

namespace MonApi.API.Addresses.Extensions;

public static class AddressExtensions
{
    public static Address MapToAddressModel(this CreateAddressDto addressDto)
    {
        return new Address
        {
            AddressLine = addressDto.AddressLine,
            City = addressDto.City,
            Country = addressDto.Country,
            ZipCode = addressDto.ZipCode,
            Complement = addressDto.Complement,
        };
    }

    public static Address MapToAddressModel(this UpdateAddressDto addressDto)
    {
        return new Address
        {
            AddressId = addressDto.AddressId,
            AddressLine = addressDto.AddressLine,
            City = addressDto.City,
            Country = addressDto.Country,
            ZipCode = addressDto.ZipCode,
            Complement = addressDto.Complement,
        };
    }
}