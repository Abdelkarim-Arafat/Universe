using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.StudyLoadRuleServices.Dtos;

public record StudyLoadRuleResponse(
    string Id,
    decimal GpaFrom,
    decimal GpaTo,
    int MinHours,
    int MaxHours
);
