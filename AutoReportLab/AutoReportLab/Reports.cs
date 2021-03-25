namespace AutoReportLab
{
    public class Reports
    {
        public Reports(Workers workers)
        {
            workers.CreateReportsFolders();
        }
    }
}