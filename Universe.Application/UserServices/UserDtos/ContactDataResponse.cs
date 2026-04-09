using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.UserDtos;

public record ContactDataResponse(
    string City, // المدينة
    string Address, // العنوان
    string PostalCode, //
    string PhoneNumber, // الموبايل
    string Email // البريد الالكتروني
);