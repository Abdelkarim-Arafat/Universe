using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Core.Entities.StudentInfo;

[Owned]
public class ContactInfo
{
    public string City { get; set; } = string.Empty; // المدينة
    public string Address { get; set; } = string.Empty; // العنوان
    public string PostalCode { get; set; } = string.Empty; //
    public string PhoneNumber { get; set; } = string.Empty; // الموبايل
    public string Email { get; set; } = string.Empty; // البريد الالكتروني
}
