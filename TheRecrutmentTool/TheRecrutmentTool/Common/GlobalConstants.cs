namespace TheRecrutmentTool.Common
{
    public static class GlobalConstants
    {
        public const int IdMaxLength = 40;
        public const int MaxCandidatesCollectionLength = 5; 
        public const string EmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";


        public class Skill
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 1;
        }

        public class Candidate
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 1;
        }

        public class Job
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 1;
            public const int DescriptionMaxLength = 1000;
            public const int DescriptionMinLength = 1;
        }
    }
}
