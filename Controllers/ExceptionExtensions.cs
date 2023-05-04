using System;

namespace sales_invoicing_dotnet.Controllers
{
    public static class ExceptionExtensions
    {
        public static int LineNumber(this Exception ex)
        {
            var lineNumber = 0;
            var lineSearch = "line";
            var index = ex.StackTrace?.IndexOf(lineSearch);
            if (index > 0)
            {
                var lineNumberText = ex.StackTrace.Substring(index.Value + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                    return lineNumber;
                }
            }
            return lineNumber;
        }
    }
}
