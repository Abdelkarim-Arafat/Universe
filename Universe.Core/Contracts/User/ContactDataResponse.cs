using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Contracts.User;

public record ContactDataResponse (
    string City, // المدينة
    string Address, // العنوان
    string PostalCode, //
    string PhoneNumber, // الموبايل
    string Email // البريد الالكتروني
);