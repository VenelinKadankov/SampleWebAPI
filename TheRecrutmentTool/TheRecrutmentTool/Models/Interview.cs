namespace TheRecrutmentTool.Models
{
    public class Interview
    {
        public int JobId { get; set; }

        public Job Job { get; set; }

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }
    }
}
