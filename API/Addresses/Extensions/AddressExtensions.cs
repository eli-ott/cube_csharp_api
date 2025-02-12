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
            Complement = addressDto.Complement
        };
    }

    public static Address MapToAddressModel(this ReturnAddressDto addressDto)
    {
        return new Address
        {
            AddressLine = addressDto.AddressLine,
            City = addressDto.City,
            Country = addressDto.Country,
            ZipCode = addressDto.ZipCode,
            Complement = addressDto.Complement
        };
    }
}