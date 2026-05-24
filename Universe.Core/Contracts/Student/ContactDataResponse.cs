namespace Universe.Core.Contracts.Student;

public record ContactDataResponse(
    string City, // المدينة
    string Address, // العنوان
    string PostalCode, //
    string PhoneNumber, // الموبايل
    string Email // البريد الالكتروني
);