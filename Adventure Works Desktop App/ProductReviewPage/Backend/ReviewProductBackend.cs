using Adventure_Works_Desktop_App.Globals.DataClasses;

namespace Adventure_Works_Desktop_App.ProductReviewPage.Backend
{
    internal class ReviewProductBackend
    {
        private readonly ProductReviewDAL _dal = new ProductReviewDAL();
        public void SubmitReview(string productID, int rating, string reviewName, string comments)
        {
            string displayName = _dal.DBGetDisplayName(TrimUsername(reviewName));
            ReviewProductData rpData = new ReviewProductData(productID, rating, displayName, comments, _dal.DBGetEmailAddress(displayName));
            _dal.DBSubmitReview(rpData);
        }

        #region Helper Methods

        /// <summary>
        /// Trims the specified username by removing any prefix up to and including the first colon.
        /// </summary>
        /// <param name="reviewName">The username string to be trimmed. Must contain a colon followed by at least one character.</param>
        /// <returns>The trimmed username, starting from the character immediately after the first colon.</returns>
        private string TrimUsername(string reviewName)
        {
            return reviewName.Substring(reviewName.IndexOf(":") + 2);
        }

        #endregion
    }
}
