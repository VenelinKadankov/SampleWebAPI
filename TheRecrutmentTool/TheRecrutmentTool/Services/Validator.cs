namespace TheRecrutmentTool.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using TheRecrutmentTool.Services.Models;

    using static Common.GlobalConstants;
    using static Common.GlobalConstants.Candidate;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateCandidate(CandidateServiceModel candidate)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(candidate.FirstName) || 
                candidate.FirstName.Length < NameMinLength ||
                candidate.FirstName.Length > NameMaxLength)
            {
                errors.Add($"First name '{candidate.FirstName}' is not valid. It must be between {NameMinLength} and {NameMaxLength} characters long.");
            }

            if (string.IsNullOrEmpty(candidate.LastName) ||
                candidate.LastName.Length < NameMinLength ||
                candidate.LastName.Length > NameMaxLength)
            {
                errors.Add($"Last name '{candidate.LastName}' is not valid. It must be between {NameMinLength} and {NameMaxLength} characters long.");
            }

            if (string.IsNullOrEmpty(candidate.Email) || 
                !Regex.IsMatch(candidate.Email, EmailRegularExpression))
            {
                errors.Add($"Email '{candidate.Email}' is not a valid e-mail address.");
            }


            if (string.IsNullOrEmpty(candidate.Bio))
            {
                errors.Add($"Bio can not be null");
            }

            return errors;
        }

        public ICollection<string> ValidateJob(JobServiceModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.Title) ||
                string.IsNullOrEmpty(model.Description) ||
                model.Salary < 0)
            {
                errors.Add($"Invalid job data.");
            }

            return errors;
        }
    }
}
