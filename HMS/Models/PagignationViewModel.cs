using System;

namespace HMS.Models
{
    /// <summary>
    /// ASP.NET MVC - Pagination Example with Logic like Google
    /// http://jasonwatmore.com/post/2015/10/30/aspnet-mvc-pagination-example-with-logic-like-google.aspx
    /// https://github.com/SajjadArifGul/DealDouble/blob/master/DealDouble.Web/ViewModels/BaseViewModels.cs
    /// </summary>
    public class Pager
    {
        public Pager(int iTotalRec, int? iPage, int iReco = 10)
        {
            // Calculate total, start and end pages
            var totPages = (int)Math.Ceiling((decimal)iTotalRec / (decimal)iReco);
            var currentPage = iPage != null ? (int)iPage : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totPages)
            {
                endPage = totPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            RecoSize = iReco;
            TotalReco = iTotalRec;
            TotalPages = totPages;
            CurPage = currentPage;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int RecoSize { get; private set; }
        public int TotalReco { get; private set; }
        public int TotalPages { get; private set; }
        public int CurPage { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}